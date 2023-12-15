using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using System.Data;
using System.Linq;
using WeatherApp;

namespace WeatherAppUnitTests
{
    [TestClass()]
    public class AccountServicingTests
    {

        string connectionString = "Server=localhost:3306;Database=weatherappuserdata;Uid=root;Pwd=1Sully75$27062003;";

        [TestMethod()]
        public void VerifyExistingUserName()
        {
            // Arrange
            var accountServicing = new AccountServicing(connectionString);

            // Act
            bool userExists = accountServicing.verifyuserName("tparisian");

            // Assert
            Assert.IsTrue(userExists, "The user should exist");
        }
    }
}