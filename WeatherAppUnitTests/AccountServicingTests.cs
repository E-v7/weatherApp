using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Runtime;
using WeatherApp;
using WeatherApp.Properties;
using WeatherAppUnitTests.Properties;

namespace WeatherAppUnitTests
{
    [TestClass()]
    public class AccountServicingTests
    {
        private static Settings settings = new Settings();

        string connectionString = settings.ConnectionString;

        [TestMethod()]
        public void VerifyExistingUserName()
        {
            var accountServicing = new AccountServicing(connectionString);
            bool userExists = accountServicing.verifyuserName("tparisian");
            Assert.IsTrue(userExists, "The user should exist");
        }
        [TestMethod()]
        public void VerifyNonExistingUserName()
        {
            var accountServicing = new AccountServicing(connectionString);
            bool userNotExists = accountServicing.verifyuserName("ThisIsNotValid");
            Assert.IsFalse(userNotExists, "The user should not exist");
        }

        [TestMethod()]
        public void VerifyExistingPassword()
        {
            var accountServicing = new AccountServicing(connectionString);
            bool PasswordExists = accountServicing.VerifyLogin("Thomas.Audet", "1234Thomas");
            Assert.IsTrue(PasswordExists, "the Password should exist");
        }

        [TestMethod()]
        public void VerifyNonExistingPasswordAndNonExistingUserName()
        {

            var accountServicing = new AccountServicing(connectionString);
            bool PasswordNotExists = accountServicing.VerifyLogin("ThisIsNotValid", "AlsoNotValid");
            Assert.IsFalse(PasswordNotExists, "The password should not exist");
        }

        [TestMethod()]
        public void VerifyExistingPasswordAndNonExistingUserName()
        {
            var accountServicing = new AccountServicing(connectionString);
            bool PasswordNotExists = accountServicing.VerifyLogin("ThisIsNotValid", "1234Thomas");
            Assert.IsFalse(PasswordNotExists, "The password should not exist");
        }

        [TestMethod()]
        public void VerifyExistingUserNameAndNonExistingPassword()
        {
            var accountServicing = new AccountServicing(connectionString);
            bool PasswordNotExists = accountServicing.VerifyLogin("Thomas.Audet", "AlsoNotValid");
            Assert.IsFalse(PasswordNotExists, "The password should not exist");
        }


        /*[TestMethod()]
        public void verifyCreatingUserAllCorrectInfo()
        {
            var accountServicing = new AccountServicing(connectionString);
            bool AccountCreationValid = accountServicing.CreateAccount("taudet0000", "Thomas12345$", "taudet@example.com");
            Assert.IsTrue(AccountCreationValid, "The account was created");
        }*/

        [TestMethod()]
        public void verifyCreatingUserpasswordnotStrong()
        {
            var accountServicing = new AccountServicing(connectionString);
            bool AccountCreationValid = accountServicing.CreateAccount("taudet0000", "Thomas", "taudet@example.com");
            Assert.IsFalse(AccountCreationValid, "The account should not be created");
        }



        /*[TestMethod()]
        public void VerifyAddingSavedLocation()
        {
            var accountServicing = new AccountServicing(connectionString);
            bool locationAdded = accountServicing.AddSavedLocation("Milton", "ON", 2);
            Assert.IsTrue(locationAdded, "The location should be added");
        }*/

        [TestMethod()]
        public void VerifyAddingExistingID()
        {
            var accountServicing = new AccountServicing(connectionString);
            bool locationAdded = accountServicing.AddSavedLocation("Milton", "ON", 201);
            Assert.IsFalse(locationAdded, "The location should not added");
        }

        [TestMethod()]
        public void VerifyAddingNegativeID()
        {
            var accountServicing = new AccountServicing(connectionString);
            bool locationAdded = accountServicing.AddSavedLocation("Milton", "ON", -202121);
            Assert.IsFalse(locationAdded, "The location should not added");
        }

        [TestMethod()]
        public void VerifyAddingHigherID()
        {
            var accountServicing = new AccountServicing(connectionString);
            bool locationAdded = accountServicing.AddSavedLocation("Milton", "ON", 99999999);
            Assert.IsFalse(locationAdded, "The location should not added");
        }

        [TestMethod()]
        public void VerifyAddHistoryLocation()
        {
            var accountServicing = new AccountServicing(connectionString);
            bool locationAdded = accountServicing.AddHistoryLocation("Milton", "ON", 201);
            Assert.IsTrue(locationAdded, "the location should be added");
        }

        [TestMethod()]
        public void VerifyAddHistoryLocationNegativeID()
        {
            var accountServicing = new AccountServicing(connectionString);
            bool locationAdded = accountServicing.AddHistoryLocation("Milton", "ON", -201);
            Assert.IsFalse(locationAdded, "the location should not be added");
        }

        [TestMethod()]
        public void VerifyAddHistoryLocationHigherID()
        {
            var accountServicing = new AccountServicing(connectionString);
            bool locationAdded = accountServicing.AddHistoryLocation("Milton", "ON", 99999999);
            Assert.IsFalse(locationAdded, "the location should not be added");
        }

    }
}