using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Components
{
    public class Account
    {
        #region Attributes and Constructor
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Dob { get; set; }
        public Account(string email, string name, string password, string dob)
        {
            Email = email;
            Name = name;
            Password = password;
            Dob = dob;
        }
        #endregion
        #region Static Methods
        public static byte[] ComputeSalt()
        {
            Random random = new Random();
            int saltSize = random.Next(4, 8);
            byte[] saltBytes = new byte[saltSize];

            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetNonZeroBytes(saltBytes);

            return saltBytes;
        }

        public static Dictionary<string, string> ComputeHash(string pass, string definedSalt)
        {
            byte[] salt = Convert.FromBase64String(definedSalt);
            if (String.IsNullOrEmpty(definedSalt))
            {
                salt = ComputeSalt();
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
