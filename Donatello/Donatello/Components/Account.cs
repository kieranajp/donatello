using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Donatello
{
    /// <summary>
    /// This class is an Account object, which is instantiated for account manipulation.
    /// </summary>
    public class Account
    {
        #region Attributes and Constructor
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Dob { get; set; }
        /// <summary>
        /// The constructor for this class. Assigns parameters to variables.
        /// </summary>
        /// <param name="email">String: Account email address. Also functions as unique identifier.</param>
        /// <param name="name">String: User's name.</param>
        /// <param name="password">String: User's password. Only briefly stored in memory - very quickly hashed.</param>
        /// <param name="dob">String: User's date of birth. Used for age verification when purchasing.</param>
        public Account(string email, string name, string password, string dob)
        {
            Email = email;
            Name = name;
            Password = password;
            Dob = dob;
        }
        #endregion
        #region Static Methods
        /// <summary>
        /// Computes a random salt of between 4 and 8 butes in size.
        /// </summary>
        /// <returns>Array of bytes: A small array of bytes to function as a salt.</returns>
        public static byte[] ComputeSalt()
        {
            Random random = new Random();
            int saltSize = random.Next(4, 8);
            byte[] saltBytes = new byte[saltSize];

            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetNonZeroBytes(saltBytes);

            return saltBytes;
        }
        /// <summary>
        /// Computes a hash from a password and optionally a salt. If no salt is specified, one is generated.
        /// </summary>
        /// <param name="pass">String: Plain-text password to encrypt.</param>
        /// <param name="definedSalt">String: Optional: a salt, if one is being explicitly provided.</param>
        /// <returns>Dictionary of strings against strings: A dictionary containing the password and the salt.</returns>
        public static Dictionary<string, string> ComputeHash(string pass, string definedSalt)
        {
            byte[] salt;
            if (String.IsNullOrEmpty(definedSalt))
            {
                salt = ComputeSalt();
            }
            else
            {
                salt = Convert.FromBase64String(definedSalt);
            }
            
            byte[] passBytes = Encoding.UTF8.GetBytes(pass);
            byte[] passAndSalt = new byte[passBytes.Length + salt.Length];

            for (int i = 0; i < passBytes.Length; i++)
            {
                passAndSalt[i] = passBytes[i];
            }

            for (int i = 0; i < salt.Length; i++)
            {
                passAndSalt[passBytes.Length + i] = salt[i];
            }

            HashAlgorithm algo = new SHA512Managed();
            byte[] byteHash = algo.ComputeHash(passAndSalt);

            byte[] saltedHash = new byte[byteHash.Length + salt.Length];

            for (int i = 0; i < byteHash.Length; i++)
            {
                saltedHash[i] = byteHash[i];
            }
            for (int i = 0; i < salt.Length; i++)
            {
                saltedHash[byteHash.Length + i] = salt[i];
            }

            Dictionary<string, string> hashAndSalt = new Dictionary<string, string>();
            hashAndSalt.Add("hash", Convert.ToBase64String(saltedHash));
            hashAndSalt.Add("salt", Convert.ToBase64String(salt));

            return hashAndSalt;
        }
        #endregion
    }
}
