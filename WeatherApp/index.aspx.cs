using Microsoft.SqlServer.Server;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using WeatherApp.Properties;

namespace WeatherApp
{
    public partial class index : System.Web.UI.Page
    {
        //protected AccountServicing accountService;

        protected void Page_Load(object sender, EventArgs e)
        {
            //accountService = new AccountServicing();
            // additional page load logic if necessary
        }
        [WebMethod]
        public static string GetWeather(string lat, string lon)
        {
            // call the WeatherWizard class method to get the current weather
            JObject weatherData = WeatherWizard.GetCurrentWeatherToJObject(lat, lon);

            // convert the JObject to a JSON string to send back to the client
            return weatherData.ToString();
        }

        public void Login_Click(object sender, EventArgs e)
        {
            string username = userName.Text.Trim();
            string password = passWord.Text;

            // Call AccountServicing methods for validation (to be implemented)
            // This will eventually check if the user exists in the database and validate the password

            bool isValidUser = accountService.VerifyLogin(username, password);

            //bool isValidUser = accountService.ValidateLogin(username, password);
            bool isValidUser = false;


            if (isValidUser)
            {
                // proceed with successful login logic
                // for example: Redirect to a different page or display a success message
            }
            else
            {
                // handle failed login attempt
                // display an error message to the user
            }


        }

        public void Register_Click(object sender, EventArgs e)
        {
            string firstname = firstName.Text.Trim();
            string lastname = lastName.Text.Trim();
            string email = emailAddress.Text.Trim();
            string username = userName.Text.Trim();
            string password = passWord.Text.Trim();
            string confirmPw = confirmPassword.Text.Trim();

            if (password == confirmPw)
            {

            }
        }
    }
}