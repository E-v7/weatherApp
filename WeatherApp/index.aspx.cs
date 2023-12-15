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

        protected void Page_Load(object sender, EventArgs e)
        {
            //accountService = new AccountServicing();
            
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
         * DESCRIPTION   :
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
            string confPassword = confirmPassword.Text;

            if (password != confPassword)
            {
                // Passwords do not match
                // Display error message
                // ErrorMessage.Text = "Passwords do not match.";
                // ErrorMessage.Visible = true;
                return;
            }

            // Attempt to create a new account
            if (accountService.CreateAccount(username, password, email))
            {
                // Account creation successful
                // Possibly redirect to a welcome page or login page
                // Response.Redirect("Welcome.aspx");
            }
            else
            {
                // Account creation failed
                // Display error message
                // ErrorMessage.Text = "Failed to create account. Please try again.";
                // ErrorMessage.Visible = true;
            }
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
            string searchQuery = Search.Text.Trim();

            // splitting user's search by comma if they enter all three parameters 
            string[] searchParts = searchQuery.Split(',');
            JObject weatherData = null;

            // determines the number of parts and call the appropriate WeatherWizard method
            if (searchParts.Length == 1)
            {
                // city name only
                weatherData = WeatherWizard.GetCurrentWeatherToJObject(searchParts[0].Trim());
            }
            else if (searchParts.Length == 2)
            {
                // city and country code
                weatherData = WeatherWizard.GetCurrentWeatherToJObject(searchParts[0].Trim(), searchParts[1].Trim());
            }
            else if (searchParts.Length == 3)
            {
                // city, state code, and country code
                weatherData = WeatherWizard.GetCurrentWeatherToJObject(searchParts[0].Trim(), searchParts[2].Trim(), searchParts[1].Trim());
            }

            if (weatherData != null)
            {
                WeatherInfo.Text = weatherData.ToString(Formatting.None);
                WeatherInfo.Visible = true;
            }
            else
            {
                WeatherInfo.Text = "Weather information could not be retrieved.";
                WeatherInfo.Visible = true;
            }
        }

    }
}