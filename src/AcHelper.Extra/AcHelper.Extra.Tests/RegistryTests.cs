using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Win32;

namespace AcHelper.Extra.Tests
{
    using NUnit.Framework;

    /// <summary>
    /// Summary description for JustMockTest
    /// </summary>
    [TestClass]
    public class RegistryTests
    {
        private const string SUBKEY_SOFTWARE = "SOFTWARE";
        private const string SUBKEY_TEMP_ACHELPER = @"TEMP_AcHelper_Extra";

        //private TestContext testContextInstance;

        ///// <summary>
        /////Gets or sets the test context which provides
        /////information about and functionality for the current test run.
        /////</summary>
        //public TestContext TestContext
        //{
        //    get
        //    {
        //        return testContextInstance;
        //    }
        //    set
        //    {
        //        testContextInstance = value;
        //    }
        //}

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        [TestInitialize()]
        public void CreateCurrentUserTempSubKey()
        {
            // Get Software key from Current User
            RegistryKey currentuser = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(SUBKEY_SOFTWARE, true);

            // Create temporary subkey 
            if (currentuser != null)
            {
                RegistryKey key = currentuser.CreateSubKey(SUBKEY_TEMP_ACHELPER);
                key.SetValue("Hello", "Kitty");
                key.Close();
            }
        }
        //
        // Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            // Get Software key from Current User
            RegistryKey currentuser = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(SUBKEY_SOFTWARE, true);

            // Create temporary subkey 
            if (currentuser != null)
            {
                currentuser.DeleteSubKey(SUBKEY_TEMP_ACHELPER, false);
            }
        }
        #endregion

        [TestMethod]
        public void GetRegistryKey_CurrentUser_ReturnValidSoftwareKey()
        {
            // Arrange
            RegistryKey expected = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(SUBKEY_SOFTWARE);

            // Act
            RegistryHive hive = RegistryHive.CurrentUser;
            RegistryKey actual = Registry.GetRegistryKey(hive, SUBKEY_SOFTWARE);

            // Assert
            Assert.AreEqual(expected.Name, actual.Name);
        }
        [TestMethod]
        public void GetRegistryKey_LocalMachine_ReturnValidSoftwareKey()
        {
            // Arrange
            RegistryKey expected = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(SUBKEY_SOFTWARE);

            // Act
            RegistryHive hive = RegistryHive.LocalMachine;
            RegistryKey actual = Registry.GetRegistryKey(hive, SUBKEY_SOFTWARE);

            // Assert
            Assert.AreEqual(expected.Name, actual.Name);
        }
        [TestCase(RegistryHive.CurrentUser)]
        [TestCase(RegistryHive.LocalMachine)]
        public void GetRegistryKey_ReturnValidSoftwareKey(RegistryHive hive)
        {
            // Arrange
            Microsoft.Win32.RegistryKey keyhive;
            switch (hive)
            {
                case RegistryHive.CurrentUser:
                    keyhive = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.CurrentUser, RegistryView.Default);
                    break;
                case RegistryHive.LocalMachine:
                    keyhive = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Default);
                    break;
                default:
                    Assert.Fail("Wrong RegistryHive entered.");
                    return;
            }

            RegistryKey expected = keyhive.OpenSubKey(SUBKEY_SOFTWARE);

            // Act
            RegistryKey actual = Registry.GetRegistryKey(hive, SUBKEY_SOFTWARE);

            // Assert
            Assert.AreEqual(expected.Name, actual.Name);
        }

        [TestCase(RegistryHive.CurrentUser, ExpectedResult = true)]
        [TestCase(RegistryHive.LocalMachine, ExpectedResult = true)]
        public bool CheckKeyExistance(RegistryHive hive)
        {
            // Arrange
            Microsoft.Win32.RegistryKey keyhive;
            switch (hive)
            {
                case RegistryHive.CurrentUser:
                    keyhive = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.CurrentUser, RegistryView.Default);
                    break;
                case RegistryHive.LocalMachine:
                    keyhive = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Default);
                    break;
                default:
                    Assert.Fail("Wrong RegistryHive entered.");
                    return false;
            }

            // Act
            return Registry.CheckKeyExistance(hive, SUBKEY_SOFTWARE);
        }
    }
}
