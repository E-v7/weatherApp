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
            bool userExists = accountServicing.verifyuserName("ThisIsNotValid");
            Assert.IsFalse(userExists, "The user should not exist");
        }

        [TestMethod()]
        public void VerifyExistingPassword()
        {
            var accountServicing = new AccountServicing(connectionString);
            bool userExists = accountServicing.VerifyPassword("Thomas.Audet", "1234Thomas");
            Assert.IsTrue(userExists, "the Password should exist");
        }

        [TestMethod()]
        public void VerifyNonExistingPasswordAndNonExistingUserName()
        {

            var accountServicing = new AccountServicing(connectionString);
            bool userExists = accountServicing.VerifyPassword("ThisIsNotValid", "AlsoNotValid");
            Assert.IsFalse(userExists, "The password should not exist");
        }

        [TestMethod()]
        public void VerifyExistingPasswordAndNonExistingUserName()
        {
            var accountServicing = new AccountServicing(connectionString);
            bool userExists = accountServicing.VerifyPassword("ThisIsNotValid", "1234Thomas");
            Assert.IsFalse(userExists, "The password should not exist");
        }

        [TestMethod()]
        public void VerifyExistingUserNameAndNonExistingPassword()
        {
            var accountServicing = new AccountServicing(connectionString);
            bool userExists = accountServicing.VerifyPassword("Thomas.Audet", "AlsoNotValid");
            Assert.IsFalse(userExists, "The password should not exist");
        }
    }
}