using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.WebControls;

namespace WeatherApp
{
    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }

    public class AccountServicing
    {
        private readonly List<User> users = new List<User>(); // Simulated database

        // Strong password regex pattern
        private readonly Regex passwordRegex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{12,}$");

        public bool ValidateLogin(string username, string password)
        {

            return PlaceHolder;
        }

    }
}