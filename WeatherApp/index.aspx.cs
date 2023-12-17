using Amazon.Runtime.Internal.Util;
using Microsoft.SqlServer.Server;
using MongoDB.Driver.Core.Configuration;
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
        protected AccountServicing accountService;

        private static Settings settings = new Settings();

        protected void Page_Load(object sender, EventArgs e)
        {
            accountService = new AccountServicing(settings.ConnectionString);

            if (Session["LastSearch"] != null) {
                ParseAndDisplayWeatherData(Session["LastSearch"].ToString());
            }
        }
        [WebMethod]
        public static string GetWeather(double lat, double lon)
        {
            // call the WeatherWizard class method to get the current weather
            JObject weatherData = WeatherWizard.GetCurrentWeatherToJObject(lat, lon);

            // convert the JObject to a JSON string to send back to the client
            return weatherData.ToString();
        }

        /*
         * FUNCTION      : Login_Click(object sender, EventArgs e)
         *
         * DESCRIPTION   : This function uses HTML geolocation api built into webforms if HTTPS is selected
         *                 initially 
         * 
         * PARAMETERS    : NONE 
         * 
         * RETURNS       : NONE
         * 
         */
        protected void Login_Click(object sender, EventArgs e)
        {
            // retrieve the user input from the text boxes
            string username = userName.Text.Trim();
            string password = passWord.Text;

            // use the AccountServicing instance to verify the login
            bool isValidUser = accountService.VerifyLogin(username, password);

            if (isValidUser)
            {
                // store the username in a session variable for later use
                Session["Username"] = username;

                // get the username and greet them 
                Greeting.Text = $"Hi {username}! Do you want to search a forecast today?";
                Greeting.Visible = true;

                // hide the login UI calls 'hideLoginUI' in script.js
                // this will update the display style of the loginContainer to 'none'
                ScriptManager.RegisterStartupScript(this, GetType(), "HideLoginScript", "hideLoginUI();", true);

            }
            else
            {
                // maybe exception 
            }
        }



        /*
         * FUNCTION      : Register_Click(object sender, EventArgs e)
         *
         * DESCRIPTION   : uses CreateAccount method from the AccountServicing class in order to verify 
         *                 username and email and password used to create the account. This click event
         *                 specifically hides the register part of the login-container and prompts user
         *                 to log in with their newly created credentials. 
         * 
         * PARAMETERS    : NONE 
         * 
         * RETURNS       : NONE
         * 
         */
        protected void Register_Click(object sender, EventArgs e)
        {
            string email = emailAddress.Text.Trim();
            string username = regUsername.Text.Trim();
            string password = regPassword.Text;

            // attempt to create a new account uses AccountServicing class
            bool isAccountCreated = accountService.CreateAccount(username, password, email);

            if (isAccountCreated)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "HideRegistrationForm", "hideRegistrationForm();", true);

                // login section prompts user to log in with the new credentials
                loginMessage.Text = "Your account was successfully created. Please log in with your new credentials.";
                loginMessage.Visible = true;
                
                ClearRegistrationForm();
            }
            else
            {
                // account creation failed, show error message
            }
        }

        // method to clear the registration form fields
        private void ClearRegistrationForm()
        {
            emailAddress.Text = string.Empty;
            regUsername.Text = string.Empty;
            regPassword.Text = string.Empty;
            confirmPassword.Text = string.Empty;
        }

        /*
         * FUNCTION      :Search_Click(object sender, EventArgs e)
         *
         * DESCRIPTION   :Search button click event handler that calls a method RequestCurrentWeatherAPI
         *                from WeatherWizard class and pulls the weather forecast for the location being 
         *                entered in the search bar. 
         * 
         * PARAMETERS    :NONE 
         * 
         * RETURNS       :NONE
         * 
         */
        protected void Search_Click(object sender, EventArgs e)
        {
            string searchQuery = Request.Form["city"];

            ParseAndDisplayWeatherData(searchQuery);
        }

        /*
         * 
         */
        private void ParseAndDisplayWeatherData(string searchQuery) {
            // splitting user's search by comma if they enter all three parameters 
            string[] searchParts = searchQuery.Split(',');
            JObject weatherData = null;

            // determines the number of parts and call the appropriate WeatherWizard method
            if (searchParts.Length == 1) {
                // city name only
                weatherData = WeatherWizard.GetCurrentWeatherToJObject(searchParts[0].Trim());
            } else if (searchParts.Length == 2) {
                // city and country code
                weatherData = WeatherWizard.GetCurrentWeatherToJObject(searchParts[0].Trim(), searchParts[1].Trim());
            } else if (searchParts.Length == 3) {
                // city, state code, and country code
                weatherData = WeatherWizard.GetCurrentWeatherToJObject(searchParts[0].Trim(), searchParts[2].Trim(), searchParts[1].Trim());
            }

            if (weatherData != null) {
                // convert pulled weather data to JSON string
                string weatherJson = weatherData.ToString(Formatting.None);

                // call the displayWeather from script.js
                string script = $"displayWeather('{weatherJson}');";
                ScriptManager.RegisterStartupScript(this, GetType(), "DisplayWeather", script, true);

                // hides the login UI if the user is logged in
                if (Session["Username"] != null) {
                    ScriptManager.RegisterStartupScript(this, GetType(), "HideLogin", "hideLoginUI();", true);
                }
                Session["LastSearch"] = searchQuery;
            } else {
                WeatherInfo.Text = "Weather information could not be retrieved.";
                WeatherInfo.Visible = true;
            }
        }
    }
}
