using Donatello;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace UnitTests
{
    /// <summary>
    ///This is a test class for AccountTest and is intended
    ///to contain all AccountTest Unit Tests
    ///</summary>
    [TestClass()]
    public class AccountTest
    {
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
        /// <summary>
        ///A test for Account Constructor
        ///</summary>
        [TestMethod()]
        public void AccountConstructorTest()
        {
            string email = "email@address.com";
            string name = "bob";
            string password = "123";
            string dob = "11-10-1989";
            Account target = new Account(email, name, password, dob);

            Assert.AreEqual(email, target.Email);
            Assert.AreEqual(name, target.Name);
            Assert.AreEqual(password, target.Password);
            Assert.AreEqual(dob, target.Dob);
        }
        /// <summary>
        ///A test for ComputeHash
        ///</summary>
        [TestMethod()]
        public void ComputeHashTest()
        {
            string pass = "123";
            string definedSalt = "r2VICQ==";
            
            Dictionary<string, string> actual = Account.ComputeHash(pass, definedSalt);

            Assert.AreEqual(definedSalt, actual["salt"]);
            Assert.AreEqual(DbConnect.GetPassHash("test@test.com"), actual["hash"]);
        }
        /// <summary>
        ///A test for ComputeSalt
        ///</summary>
        [TestMethod()]
        public void ComputeSaltTest()
        {
            byte[] salt1 = Account.ComputeSalt();
            byte[] salt2 = Account.ComputeSalt();
            Assert.AreNotEqual(salt1, salt2);
        }
    }
}
