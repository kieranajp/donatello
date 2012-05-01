using Donatello;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data;

namespace UnitTests
{
    
    
    /// <summary>
    ///This is a test class for DbConnectTest and is intended
    ///to contain all DbConnectTest Unit Tests
    ///</summary>
    [TestClass()]
    public class DbConnectTest
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

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        /// <summary>
        ///A test for AccountExists
        ///</summary>
        [TestMethod()]
        public void AccountExistsTest()
        {
            string email = "test@test.com";
            string fake = "no";
            bool expected = true;
            bool actual;

            actual = DbConnect.AccountExists(email);
            Assert.AreEqual(expected, actual);

            actual = DbConnect.AccountExists(fake);
            Assert.AreNotEqual(expected, actual);
        }

        /// <summary>
        ///A test for CalculateMD5
        ///</summary>
        [TestMethod()]
        public void CalculateMD5Test()
        {
            string input = "1";
            string expected = "c4ca4238a0b923820dcc509a6f75849b";
            string actual;

            actual = DbConnect.CalculateMD5(input);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for CheckLogin
        ///</summary>
        [TestMethod()]
        public void CheckLoginTest()
        {
            string email = "test@test.com";
            string hash = "V6XG5WytBR8fb9txtcRWDAYKzHLC5pnN9oOXuPm05YKX9v6B2xa9/4RgSDDw0j/ICnqP3z+0hApjDm2iyclOla9lSAk=";
            bool expected = true;
            bool actual;

            actual = DbConnect.CheckLogin(email, hash);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for CountLocations
        ///</summary>
        [TestMethod()]
        public void CountLocationsTest()
        {
            int expected = -1;
            int actual;
            actual = DbConnect.CountLocations();

            Assert.IsTrue(actual > expected);
        }

        /// <summary>
        ///A test for GetAccount
        ///</summary>
        [TestMethod()]
        public void GetAccountTest()
        {
            string id = "test@test.com";
            Account expected = new Account("test@test.com", "Mister Testy", "V6XG5WytBR8fb9txtcRWDAYKzHLC5pnN9oOXuPm05YKX9v6B2xa9/4RgSDDw0j/ICnqP3z+0hApjDm2iyclOla9lSAk=", "17-08-1964");
            Account actual;
            actual = DbConnect.GetAccount(id);

            Assert.AreEqual(expected.Email, actual.Email);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Password, actual.Password);
            Assert.AreEqual(expected.Dob, actual.Dob);
        }

        /// <summary>
        ///A test for GetLocation
        ///</summary>
        [TestMethod()]
        public void GetLocationTest()
        {
            string expected = "c4ca4238a0b923820dcc509a6f75849b";
            string actual;
            actual = DbConnect.GetLocation(1);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetLocationFromProductId
        ///</summary>
        [TestMethod()]
        public void GetLocationFromProductIdTest()
        {
            int pid = 1;
            string expected = "c4ca4238a0b923820dcc509a6f75849b";
            string actual;
            actual = DbConnect.GetLocationFromProductId(pid);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// A test for GetLocation
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetInvalidLocationTest()
        {
            int input = 0;
            string actual;
            actual = DbConnect.GetLocation(input);
        }

        /// <summary>
        /// A test for GetLocationFromProductId
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetInvalidLocationFromProductIdTest()
        {
            int input = 0;
            string actual;
            actual = DbConnect.GetLocationFromProductId(input);
        }

        /// <summary>
        ///A test for GetMD5Hash
        ///</summary>
        [TestMethod()]
        public void GetMD5HashTest()
        {
            string product = "Battlefield 3";
            string expected = "7033166343ae590e9114df94fe1e803b";
            string actual;
            actual = DbConnect.GetMD5Hash(product);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetPassHash
        ///</summary>
        [TestMethod()]
        public void GetPassHashTest()
        {
            string email = "test@test.com";
            string expected = "V6XG5WytBR8fb9txtcRWDAYKzHLC5pnN9oOXuPm05YKX9v6B2xa9/4RgSDDw0j/ICnqP3z+0hApjDm2iyclOla9lSAk=";
            string actual;
            actual = DbConnect.GetPassHash(email);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetProductNameFromLocationHash
        ///</summary>
        [TestMethod()]
        public void GetProductNameFromLocationHashTest()
        {
            string location = "c81e728d9d4c2f636f067f89cc14862c";
            string expected = "Battlefield 3";
            string actual;
            actual = DbConnect.GetProductNameFromLocationHash(location);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Login
        ///</summary>
        [TestMethod()]
        public void LoginTest()
        {
            string uid = "test@test.com";
            string pwd = "123";
            
            Assert.IsTrue(DbConnect.Login(uid, pwd));
        }

        /// <summary>
        ///A test for Login
        ///</summary>
        [TestMethod()]
        public void WrongPassLoginTest()
        {
            string uid = "test@test.com";
            string pwd = "abc";

            Assert.IsFalse(DbConnect.Login(uid, pwd));
        }

        /// <summary>
        ///A test for Login
        ///</summary>
        [TestMethod()]
        public void WrongUserLoginTest()
        {
            string uid = "email@address.com";
            string pwd = "123";

            Assert.IsFalse(DbConnect.Login(uid, pwd));
        }
    }
}
