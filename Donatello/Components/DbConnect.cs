using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using System;
using System.Security.Cryptography;

namespace Components
{
    public sealed class DbConnect
    {
        #region Attributes
        static readonly string connString = "Server=localhost;Database=fyp;Uid=root;Pwd=;";
        #endregion
        #region Static Methods
        public static void CreateAccount(Account acc)
        {
            // Salt and hash acc.Password
            Dictionary<string, string> hashAndSalt = Account.ComputeHash(acc.Password, null);
            
            // TODO: Escape account name and email

            using (MySqlConnection mcon = new MySqlConnection(connString))
            using (MySqlCommand cmd = mcon.CreateCommand())
            {
                mcon.Open();
                cmd.CommandText = "INSERT INTO accounts (account_id, pass_hash, pass_salt, account_nm, account_dob) VALUES ('" + acc.Email + "', '" + hashAndSalt["hash"] + "', '" + hashAndSalt["salt"] + "', '" + acc.Name + "', " + acc.Dob + ");";
                cmd.ExecuteNonQuery();
            }
        }

        public static string GetEmailAddress(string emailToFetch)
        {
            using (MySqlConnection mcon = new MySqlConnection(connString))
            using (MySqlCommand cmd = mcon.CreateCommand())
            {
                mcon.Open();
                cmd.CommandText = "SELECT account_id FROM accounts WHERE account_id = '" + emailToFetch + "';";
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    string email = "";
                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            email = reader.GetValue(i).ToString();
                        }
                    }

                    return email;
                }
            }
        }

        public static string GetPassHash(string email)
        {
            using (MySqlConnection mcon = new MySqlConnection(connString))
            using (MySqlCommand cmd = mcon.CreateCommand())
            {
                mcon.Open();
                cmd.CommandText = "SELECT pass_hash FROM accounts WHERE account_id = '" + email + "';";
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    string hash = "";
                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            hash = reader.GetValue(i).ToString();
                        }
                    }

                    return hash;
                }
            }
        }

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

                cmd.CommandText = "SELECT * FROM products WHERE product_id IN (";

                foreach (int id in ids)
                {
                    cmd.CommandText += id.ToString() + ", ";
                }

                cmd.CommandText.Remove(cmd.CommandText.Length - 2);
                cmd.CommandText += ");";

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                DataTable dt = new DataTable();
                da.Fill(dt);

                return dt;
            }
        }

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

        public static string GetLocation(int location)
        {
            using (MySqlConnection mcon = new MySqlConnection(connString))
            using (MySqlCommand cmd = mcon.CreateCommand())
            {
                mcon.Open();
                cmd.CommandText = "SELECT location FROM locations WHERE location_id = " + location + ";";
                return cmd.ExecuteScalar().ToString();
            }
        }

        public static DataSet Select()
        {
            return new DataSet();
        }
        #endregion
    }
}
