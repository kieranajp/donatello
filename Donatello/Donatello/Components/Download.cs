using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Security.Cryptography;

namespace Donatello
{
    /// <summary>
    /// This class handles downloading the files (over FTP) as well as checksumming them.
    /// </summary>
    class Download
    {
        #region Attributes & Constructor
        private Uri _location;
        private string _product;
        private string _target;
        private BackgroundWorker _bw;
        /// <summary>
        /// Constructor for the class. Instantiates the class and initialises its properties.
        /// </summary>
        /// <param name="location">String: The location of the file to download.</param>
        public Download(string location)
        {
            // We know the hash of the location of the file we have to get.
            // First step is to get the product name. We're also going to need the location we're downloading to.
            _product = DbConnect.GetProductNameFromLocationHash(location);

            // Could-Have: Let users choose download location
            _target = Properties.Settings.Default.DownloadDirectory;

            if (String.IsNullOrEmpty(_target))
            {
                string homePath = (Environment.OSVersion.Platform == PlatformID.Unix || Environment.OSVersion.Platform == PlatformID.MacOSX)
                    ? Environment.GetEnvironmentVariable("HOME") : Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%");
                Properties.Settings.Default.DownloadDirectory = _target = Path.Combine(homePath, @"\Downloads\Donatello\");
                Properties.Settings.Default.Save();
            }

            // If the target directory doesn't exist, we should create it.
            if (!Directory.Exists(_target))
            {
                Directory.CreateDirectory(_target);
            }

            // Finally we need to find out where we're downloading the file from, and get it.
            int locs = DbConnect.CountLocations();
            int decodedLocation = -1;

            // Iterate through each location in the database until you find the location_id that matches the hash we've got.
            // TODO: Isn't there a method in DBConnect that does the same thing?
            for (int i = 1; i < locs + 1; i++)
            {
                if (DbConnect.CalculateMD5(i.ToString()) == location)
                {
                    decodedLocation = i;
                }
            }

            if (decodedLocation == -1) { throw new NullReferenceException(); }

            // Pass the location_id to the database to get the actual location of the file we're after.
            string secretLoc = DbConnect.GetLocation(decodedLocation);

            _location = new Uri(@"ftp://kieranajp.co.uk/donatello/" + secretLoc + @"/" + secretLoc + ".file"); // Could-Have: Dynamic file types

            // We're going to need to multithread this now.
            _bw = new BackgroundWorker { WorkerReportsProgress = true };
            _bw.DoWork += GetFile;
            _bw.ProgressChanged += Progress;
            _bw.RunWorkerCompleted += Complete;
            
            _bw.RunWorkerAsync();
        }
        #endregion
        #region BackgroundWorker events
        /// <summary>
        /// The main loop of the BackgroundWorker thread (DoWork)
        /// Gets the filesize of the file to download and then the file itself.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetFile(object sender, DoWorkEventArgs e)
        {
            long fileSize = FTPGet(false);
            long returnCode = FTPGet(true);
            if (returnCode == -1)
            {
                // Then an error has occurred.
            }
        }
        /// <summary>
        /// ProcessChanged event of the BackgroundWorker
        /// Displays a notification every 10% of the download.
        /// (Note user feedback is minimal since the program is designed to be used while the user is away from their computer).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Progress(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage % 10 == 0)
            {
                Notification n = new Notification("Download of " + _product + ": " + e.ProgressPercentage + "%", false);
            }
        }
        /// <summary>
        /// The RunWorkerCompleted event of the BackgroundWorker. Runs when the BackgroundWorker has finished.
        /// MD5 checksums the download and proclaims it complete or displays an error as appropriate.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Complete(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                // Why could there be an error? Probably wouldn't be.
            }

            if (!CheckMD5(Path.Combine(_target, String.Concat(CleanFileName(_product), ".file")), _product))
            {
                Notification n = new Notification("Error: Checksum of " + _product + " failed :(", true);
                // Could-Have: More graceful dealing with this (redownloading that product perhaps)
                // Problem is, trying to do that simply could end up with a huge memory leak (millions of recursive threads, sort of fork bomb)
            }
            else
            {
                Notification n = new Notification("Download complete!", false);
                // The downlad's complete!
            }
        }
        #endregion
        #region Private Methods
        /// <summary>
        /// Initialises and runs FTP to get a file or its size.
        /// </summary>
        /// <param name="download">Boolean: True to download the file, false to merely get its size.</param>
        /// <returns>Long: If getting the filesize, the filesize in bytes.
        ///     If downloading the file, a returncode signifying the download's success or failure.</returns>
        private long FTPGet(bool download)
        {
            FtpWebRequest req = (FtpWebRequest)WebRequest.Create(_location);

            req.UsePassive = true;
            req.UseBinary = true;
            req.Credentials = new NetworkCredential("fyp@kieranajp.co.uk", "eijonu");

            if (download)
            {
                int returnCode = 0;
                req.KeepAlive = false;
                req.Method = WebRequestMethods.Ftp.DownloadFile;

                FtpWebResponse response = (FtpWebResponse)req.GetResponse();
                Stream responseStream = response.GetResponseStream();

                string newFileName = CleanFileName(_product);
                string path = Path.Combine(_target, String.Concat(newFileName, ".file"));

                if (File.Exists(path))
                {
                    string bak = "_backup" + DateTime.Now.ToString();
                    File.Move(path, path + bak);
                }

                try
                {
                    using (FileStream writeStream = File.Create(path))
                    {
                        Byte[] buffer = new Byte[2048];
                        int bytesRead = responseStream.Read(buffer, 0, 2048);

                        while (bytesRead > 0)
                        {
                            writeStream.Write(buffer, 0, bytesRead);
                            bytesRead = responseStream.Read(buffer, 0, 2048);
                        }
                    }
                }
                catch
                {
                    Notification n = new Notification("Error downloading " + _product, true);
                    returnCode = -1;
                }
                finally
                {
                    response.Close();
                } 
                
                return returnCode;
            }
            else
            {
                req.KeepAlive = true;
                req.Method = WebRequestMethods.Ftp.GetFileSize;
                long fileSize = (long)req.GetResponse().ContentLength;

                return fileSize;
            }
        }
        /// <summary>
        /// Strips illegal characters out of a filename.
        /// </summary>
        /// <param name="toClean">String: The filename that requires cleaning.</param>
        /// <returns>String: A filename with no illegal characters.</returns>
        private string CleanFileName(string toClean)
        {
            string cleaned = toClean;
            cleaned = String.Concat(toClean.Split(Path.GetInvalidFileNameChars(), StringSplitOptions.RemoveEmptyEntries));

            if (cleaned.Length > 150)
            {
                cleaned = cleaned.Substring(0, 150);
            }

            return cleaned;
        }
        /// <summary>
        /// Checks the MD5 checksum of an input file against the stored (valid) checksum in the database.
        /// </summary>
        /// <param name="fileToCheck">String: The location on disk of the file to checksum</param>
        /// <param name="serverFile">String: The product name of the database entry to check against</param>
        /// <returns>Boolean: True if the checksums match, false if they don't.</returns>
        private bool CheckMD5(string fileToCheck, string serverFile)
        {
            string serverHash = DbConnect.GetMD5Hash(serverFile);

            FileStream fs = new FileStream(fileToCheck, FileMode.Open);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] retVal = md5.ComputeHash(fs);
            fs.Close();

            string clientHash = Convert.ToBase64String(retVal);

            if (clientHash != serverHash)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion
    }
}
