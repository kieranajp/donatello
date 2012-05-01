using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using MySql.Data.MySqlClient;
using System.Text;
using System.Net.Sockets;
using System.Security.Cryptography;

namespace Donatello
{
    /// <summary>
    /// This class contains methods handling all communication with and manipulation of the (MySQL) database.
    /// </summary>
    public sealed class DbConnect
    {
        #region Attributes
        // The connection string for the database.
        static readonly string connString = "Server=localhost;Database=fyp;Uid=root;Pwd=;";
        #endregion
        #region Static Methods
        /// <summary>
        /// Method to create an account in the database.
        /// </summary>
        /// <param name="acc">Account: The account to create.</param>
        public static void CreateAccount(Account acc)
        {
            // Salt and hash acc.Password
            Dictionary<string, string> hashAndSalt = Account.ComputeHash(acc.Password, null);
            
            // TODO: Escape account name and email

            using (MySqlConnection mcon = new MySqlConnection(connString))
            using (MySqlCommand cmd = mcon.CreateCommand())
            {
                mcon.Open();
                cmd.CommandText = "INSERT INTO accounts (account_id, pass_hash, pass_salt, account_nm, account_dob) VALUES ('" + acc.Email + "', '" + hashAndSalt["hash"] + "', '" + hashAndSalt["salt"] + "', '" + acc.Name + "', '" + acc.Dob + "');";
                cmd.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// Retrieves an Account object by account_id
        /// </summary>
        /// <param name="id">String: The account_id to look up</param>
        /// <returns>Account: The full account details</returns>
        public static Account GetAccount(string id)
        {
            using (MySqlConnection mcon = new MySqlConnection(connString))
            using (MySqlCommand cmd = mcon.CreateCommand())
            {
                mcon.Open();
                cmd.CommandText = "SELECT account_id, account_nm, pass_hash, DATE_FORMAT(account_dob, '%d-%m-%Y') AS account_dob FROM accounts WHERE account_id = '" + id + "';";
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                List<string> list = new List<string>();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(reader.GetString("account_id"));
                        list.Add(reader.GetString("account_nm"));
                        list.Add(reader.GetString("pass_hash"));
                        list.Add(reader.GetString("account_dob"));
                    }
                }
                Account acc;
                try 
                { 
                    acc = new Account(list[0], list[1], list[2], list[3]); 
                }
                catch 
                { 
                    throw new FormatException(); 
                }
                
                return acc;
            }
        }
        /// <summary>
        /// Method to set the current machine as the unique client for the logged-in account.
        /// </summary>
        /// <param name="account">String: The account_id to lock to this IP address.</param>
        public static void SetClient(string account)
        {
            IPAddress[] IP = Dns.GetHostAddresses(Dns.GetHostName());
            using (MySqlConnection mcon = new MySqlConnection(connString))
            using (MySqlCommand cmd = mcon.CreateCommand())
            {
                mcon.Open();
                cmd.CommandText = "SELECT COUNT(*) FROM clients WHERE account_id = '" + account + "';";

                // Find out the IP address. Here we are doing local addresses; if this were more than a proof-of-concept we'd be using full external addresses.
                IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
                string localIP = "127.0.0.1";

                foreach (IPAddress ip in host.AddressList) 
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork) 
                    {
                        localIP = ip.ToString();
                    }
                }

                if (Convert.ToInt32(cmd.ExecuteScalar()) == 0)
                {
                    cmd.CommandText = "INSERT INTO clients (account_id, system_nm, client_ip) VALUES ('" + account + "', '" + Environment.MachineName + "', INET_ATON('" + localIP + "'));";
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    cmd.CommandText = "UPDATE clients SET client_ip = INET_ATON('" + localIP + "') WHERE account_id = '" + account + "';";
                    cmd.ExecuteNonQuery();
                }
            }
        }
        /// <summary>
        /// Checks if the specified account exists already in the database.
        /// </summary>
        /// <param name="email">String: The account_id to check for.</param>
        /// <returns>Boolean: True if the account exists, false if not.</returns>
        public static bool AccountExists(string email)
        {
            using (MySqlConnection mcon = new MySqlConnection(connString))
            using (MySqlCommand cmd = mcon.CreateCommand())
            {
                mcon.Open();
                cmd.CommandText = "SELECT COUNT(*) FROM accounts WHERE account_id = '" + email + "';";
                return Convert.ToInt32(cmd.ExecuteScalar()) == 0 ? false : true;
            }
        }
        /// <summary>
        /// Gets the stored secure hash of the specified account's password.
        /// </summary>
        /// <param name="email">String: The account_id to retrieve the password of.</param>
        /// <returns>String: The securely-hashed password for this account.</returns>
        public static string GetPassHash(string email)
        {
            using (MySqlConnection mcon = new MySqlConnection(connString))
            using (MySqlCommand cmd = mcon.CreateCommand())
            {
                mcon.Open();
                cmd.CommandText = "SELECT pass_hash FROM accounts WHERE account_id = '" + email + "';";
                return cmd.ExecuteScalar().ToString();
            }
        }
        /// <summary>
        /// Checks if the supplied login information is valid
        /// </summary>
        /// <param name="email">String: The account_id to attempt to login with.</param>
        /// <param name="hash">String: The securely-hashed password to try.</param>
        /// <returns>Boolean: True if the supplied login information is valid. False otherwise.</returns>
        public static bool CheckLogin(string email, string hash)
        {
            using (MySqlConnection mcon = new MySqlConnection(connString))
            using (MySqlCommand cmd = mcon.CreateCommand())
            {
                mcon.Open();
                cmd.CommandText = "SELECT COUNT(*) FROM accounts WHERE account_id = '" + email + "' AND pass_hash = '" + hash + "';";
                return Convert.ToInt32(cmd.ExecuteScalar()) == 0 ? false : true;
            }
        }
        /// <summary>
        /// Checks if the supplied login information is valid, hashing and salting the supplied password first.
        /// </summary>
        /// <param name="uid">String: The account_id to attempt to log in.</param>
        /// <param name="pwd">String: The supplied password.</param>
        /// <returns>Boolean: True if the login information is valid. False otherwise.</returns>
        public static bool Login(string uid, string pwd)
        {
            Dictionary<string, string> userDetails = new Dictionary<string, string>();
            using (MySqlConnection mcon = new MySqlConnection(connString))
            using (MySqlCommand cmd = mcon.CreateCommand())
            {
                mcon.Open();
                cmd.CommandText = "SELECT account_id, pass_hash, pass_salt FROM accounts WHERE account_id = '" + uid + "';";
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        userDetails.Add("uid", reader.GetString("account_id"));
                        userDetails.Add("hash", reader.GetString("pass_hash"));
                        userDetails.Add("salt", reader.GetString("pass_salt"));
                    }
                }
            }

            if (userDetails.Count == 0)
            {
                // User doesn't exist
                return false;
            }

            // Hash the input password using the salt retrieved from the database
            Dictionary<string, string> hashAndSalt = Account.ComputeHash(pwd, userDetails["salt"]);
            if (hashAndSalt["hash"] == userDetails["hash"])
            {
                // Username and password are both correct - the hashes match
                return true;
            }

            // If it gets this far, the password is incorrect.
            return false;
        }
        /// <summary>
        /// Gets the stored MD5 hash of the supplied product, for checksumming.
        /// </summary>
        /// <param name="product">String: The product_id to get the hash of.</param>
        /// <returns>String: The stored MD5 hash of the product.</returns>
        public static string GetMD5Hash(string product)
        {
            using (MySqlConnection mcon = new MySqlConnection(connString))
            using (MySqlCommand cmd = mcon.CreateCommand())
            {
                mcon.Open();
                cmd.CommandText = "SELECT product_hash FROM products WHERE product_nm = '" + product + "';";
                return cmd.ExecuteScalar().ToString();
            }
        }
        /// <summary>
        /// Counts the number of rows in the locations table.
        /// </summary>
        /// <returns>Integer: The number of rows present in the table.</returns>
        public static int CountLocations()
        {
            using (MySqlConnection mcon = new MySqlConnection(connString))
            using (MySqlCommand cmd = mcon.CreateCommand())
            {
                mcon.Open();
                cmd.CommandText = "SELECT COUNT(*) FROM locations;";
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        /// <summary>
        /// Gets the products that have already been purchased by the specified account.
        /// </summary>
        /// <param name="account_id">String: The account_id to get purchases of.</param>
        /// <returns>DataTable: Blank if nothing has been purchased on the account.
        ///     Containing two columns if purchases have been made: the product_id and product name of each purchased product.</returns>
        public static DataTable GetAuthorisedProducts(string account_id)
        {
            using (MySqlConnection mcon = new MySqlConnection(connString))
            using (MySqlCommand cmd = mcon.CreateCommand())
            {
                mcon.Open();
                cmd.CommandText = "SELECT product_id FROM authentications WHERE account_id = '" + account_id + "';";
                MySqlDataReader rdr = cmd.ExecuteReader();
                List<int> ids = new List<int>();

                while (rdr.Read())
                {
                    ids.Add(rdr.GetInt32("product_id"));
                }

                rdr.Close();

                if (ids.Count < 1)
                {
                    // i.e. nothing has been purchased on this account yet
                    return new DataTable();
                }

                StringBuilder sb = new StringBuilder("SELECT product_id, product_nm FROM products WHERE product_id IN (");

                foreach (int id in ids)
                {
                    sb.Append(id.ToString());
                    sb.Append(", ");
                }

                sb.Remove(sb.Length - 2, 2);
                sb.Append(");");

                cmd.CommandText = sb.ToString();

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                DataTable dt = new DataTable();
                da.Fill(dt);

                return dt;
            }
        }
        /// <summary>
        /// Gets the name of the product by its hashed location_id.
        /// </summary>
        /// <param name="location">String: The location_id to look up, hashed.</param>
        /// <returns>String: The product name.</returns>
        public static string GetProductNameFromLocationHash(string location)
        {
            using (MySqlConnection mcon = new MySqlConnection(connString))
            using (MySqlCommand cmd = mcon.CreateCommand())
            {
                mcon.Open();
                cmd.CommandText = "SELECT product_nm FROM products WHERE location_hash = '" + location + "';";
                return cmd.ExecuteScalar().ToString();
            }
        }
        /// <summary>
        /// Gets the hashed location from the locations table by id.
        /// </summary>
        /// <param name="location">Integer: The location_id to look up.</param>
        /// <returns>String: The encrypted location of the file.</returns>
        public static string GetLocation(int location)
        {
            if (location < 1)
            {
                throw new NullReferenceException();
            }
            using (MySqlConnection mcon = new MySqlConnection(connString))
            using (MySqlCommand cmd = mcon.CreateCommand())
            {
                mcon.Open();
                cmd.CommandText = "SELECT location FROM locations WHERE location_id = " + location + ";";
                return cmd.ExecuteScalar().ToString();
            }
        }
        /// <summary>
        /// Calculates the MD5 hash of an input string.
        /// </summary>
        /// <param name="input">String: The string to hash.</param>
        /// <returns>String: An MD5 hash</returns>
        public static string CalculateMD5(string input)
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
        /// <summary>
        /// Gets the hash of a product's location by its ID. 
        /// </summary>
        /// <param name="pid">Integer: The ID of the product to look up.</param>
        /// <returns>String: Empty if no location was found or if an error occurred.
        ///     The hashed location (found by running the GetLocation method) if a location was found.</returns>
        public static string GetLocationFromProductId(int pid)
        {
            if (pid < 1)
            {
                throw new NullReferenceException();
            }
            using (MySqlConnection mcon = new MySqlConnection(connString))
            using (MySqlCommand cmd = mcon.CreateCommand())
            {
                mcon.Open();
                cmd.CommandText = "SELECT location_hash FROM products WHERE product_id = " + pid + ";";

                string location_hash = cmd.ExecuteScalar().ToString();
                int locs = DbConnect.CountLocations();
                int decodedLocation = -1;

                for (int i = 1; i < locs + 1; i++)
                {
                    if (DbConnect.CalculateMD5(i.ToString()) == location_hash)
                    {
                        decodedLocation = i;
                    }
                }

                if (decodedLocation != -1)
                {
                    return DbConnect.GetLocation(decodedLocation);
                }
                else
                {
                    return string.Empty;
                }
            }
        }
        #endregion
    }
}
