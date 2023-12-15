﻿using Microsoft.SqlServer.Server;
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
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (accountService.VerifyLogin(username, password))
            {
                // Store the username in the session state
                Session["Username"] = username;

                // Call a client-side function to update the UI after login
                string script = $@"
                    document.getElementById('loginContainer').style.display = 'none';
                    document.getElementById('welcomeMessage').innerText = 'Hi {username}! Do you want to search a forecast today?';
                    document.getElementById('welcomeMessage').style.display = 'block';";
                ScriptManager.RegisterStartupScript(this, GetType(), "PostLoginUI", script, true);
            }
            else
            {
                // Display an error message on the UI
                ErrorMessage.Text = "Invalid username or password.";
                ErrorMessage.Visible = true;
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

            string email = Email.Text.Trim();
            string username = Username.Text.Trim();
            string password = regPassword.Text;
            string confirmPassword = ConfirmPassword.Text;

            if (password != confirmPassword)
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

            JObject weatherData = WeatherWizard.RequestCurrentWeatherAPI(searchQuery);
            if (weatherData != null)
            {
                WeatherInfo.Text = weatherData.ToString();
                WeatherInfo.Visible = true;
            }
            else
            {
                WeatherInfo.Text = "Weather forecast not available.";
                WeatherInfo.Visible = true;
            }
        }
    }
}