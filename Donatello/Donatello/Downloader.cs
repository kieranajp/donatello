using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Windows.Forms;
using Components;
using System.Text;

namespace Donatello
{
    class Downloader
    {
        public void Get(string location)
        {
            int locs = DbConnect.CountLocations();
            int decodedLocation = -1;

            for (int i = 1; i < locs+1; i++)
            {
                if (CalculateMD5(i.ToString()) == location)
                {
                    decodedLocation = i;
                }
            }

            if (decodedLocation == -1) { throw new NullReferenceException(); }

            string product = DbConnect.GetProductNameFromLocationHash(location);
            string actualLocation = DbConnect.GetLocation(decodedLocation);

            try
            {
                Uri secretLoc = new Uri(@"ftp://kieranajp.co.uk/donatello/" + location + @"/" + location + ".file"); // TODO: Dynamic file types

                // Could-Have: Let users choose download location
                if (String.IsNullOrEmpty(Properties.Settings.Default.DownloadDirectory))
                {
                    string homePath = (Environment.OSVersion.Platform == PlatformID.Unix || Environment.OSVersion.Platform == PlatformID.MacOSX)
                        ? Environment.GetEnvironmentVariable("HOME") : Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%");
                    Properties.Settings.Default.DownloadDirectory = homePath + @"\Downloads\Donatello\";
                }

                string saveFile = Properties.Settings.Default.DownloadDirectory + product + ".file";

                FtpWebRequest req = (FtpWebRequest)WebRequest.Create(secretLoc);

                req.KeepAlive = true;
                req.UsePassive = true;
                req.UseBinary = true;
                req.Method = WebRequestMethods.Ftp.DownloadFile;
                req.Credentials = new NetworkCredential("fyp@kieranajp.co.uk", "eijonu");

                using (FtpWebResponse response = (FtpWebResponse)req.GetResponse())
                using (Stream responseStream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(responseStream))
                using (StreamWriter destination = new StreamWriter(saveFile))
                {
                    if (response.StatusCode != FtpStatusCode.CommandOK)
                    {
                        Console.Write(response.StatusCode + ": " + response.StatusDescription);
                    }
                    destination.Write(reader.ReadToEnd());
                    destination.Flush();
                }

                /*WebClient wc = new WebClient();
                wc.DownloadFileCompleted += (sender, e) => Completed(sender, e, Properties.Settings.Default.DownloadDirectory + location, product); //new AsyncCompletedEventHandler(Completed);
                wc.DownloadProgressChanged += (sender, e) => Progress(sender, e, product);
                
                wc.DownloadFileAsync(secretLoc, Properties.Settings.Default.DownloadDirectory);*/
                Notification n = new Notification();
                n.Notify("Download started");

                // Would-have: Security - using SSL/TLS/SFTP/_something_ instead - could be done with FtpWebRequest & req.EnableSsl = true; - I just need a server that supports this stuff ;)
            }
            catch (WebException e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void Completed(object s, AsyncCompletedEventArgs e, string fileLoc, string product)
        {
            Notification n = new Notification();
            if (!CheckMD5(fileLoc, product))
            {
                n.PermaNotify("Error: Checksum of " + product + " failed :(");
                // TODO: Work out how to deal with this
            }
            else
            {
                n.Notify("Download of " + product + " complete!");
            }
        }

        private void Progress(object s, DownloadProgressChangedEventArgs e, string product)
        {
            if (e.ProgressPercentage % 10 == 0)
            {
                Notification n = new Notification();
                n.Notify("Download of " + product + " " + e.ProgressPercentage.ToString());
            }
        }

        public string CalculateMD5(string input)
        {
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                // Converting to hexadecimal lower-case representation (to match hashes generated in PHP where this is SO MUCH EASIER jeez.)
                sb.Append(hash[i].ToString("x2"));
            }
            return sb.ToString();
        }

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
    }
}
