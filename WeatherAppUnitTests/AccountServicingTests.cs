using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using System.Data;
using System.Linq;
using WeatherApp;
using WeatherApp.Properties;

namespace WeatherAppUnitTests
{
    [TestClass()]
    public class AccountServicingTests
    {

        string connectionString = "Server=localhost;Database=weatherappuserdata;Uid=Thomas;Pwd=1234.weather;";

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


        [TestMethod()]
        public void verifyCreatingUserAllCorrectInfo()
        {
            var accountServicing = new AccountServicing(connectionString);
            bool AccountCreationValid = accountServicing.CreateAccount("taudet0000", "Thomas12345$", "taudet@example.com");
            Assert.IsTrue(AccountCreationValid, "The password should not exist");
        }

        [TestMethod()]
    }
}