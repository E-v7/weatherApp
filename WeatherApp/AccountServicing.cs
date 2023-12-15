using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Transactions;
using Dapper;
using MySql.Data.MySqlClient;
using WeatherApp;

namespace WeatherApp
{
    public class User
    {
        public int userID {  get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        // Additional properties can be added here as needed
    }

    public class Password
    {
        public string password { get; set; }

        public int userID { get; set; }
    }





    public class AccountServicing
    {
        private readonly List<User> users = new List<User>(); // Simulated database

        private readonly string connectionString;
        // Strong password regex pattern
        private readonly Regex passwordRegex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{12,}$");

        public AccountServicing(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public bool CreateAccount(string username, string password, string email)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(email))
            {
                return false;
                throw new ArgumentException("Username, password, and email cannot be blank.");
            }
            if (!passwordRegex.IsMatch(password))
            {
                return false;
                throw new ArgumentException("Password does not meet security requirements.");
            }
            if (VerifyLogin(username, password) == true)
            {
                return false;
                throw new ArgumentException("An account with this username or email already exists.");
            }

            using (IDbConnection connection = new MySqlConnection(getConnectionString()))
            {
                connection.Open();
                string hashOfEnteredPassword = HashPassword(password);

                using (var transaction = connection.BeginTransaction())
                {
                    connection.Execute("INSERT INTO `userInfo` (`userName`, `userEmail`) VALUES (@Username, @Email)", new { Username = username, Email = email });

                    int userID = connection.QuerySingle<int>("SELECT LAST_INSERT_ID()");

                    connection.Execute("INSERT INTO `userPassword` (`userPassword`, `userID`) VALUES (@UserPassword, @UserID)", new { UserPassword = hashOfEnteredPassword, UserID = userID });
                    transaction.Commit();
                    return true;
                }
            }
        }
        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        public bool VerifyLogin(string userName, string enteredPassword)
        {
            using (IDbConnection connection = new MySqlConnection(getConnectionString()))
            {
                connection.Open();
                string hashOfEnteredPassword = HashPassword(enteredPassword);
                List<string> userPassword = connection.Query<string>($"SELECT up.userPassword FROM userPassword AS up INNER JOIN userInfo AS ui ON up.userID = ui.userID WHERE ui.userName = '{userName}' AND userPassword = '{hashOfEnteredPassword}'").ToList();

                if(userPassword.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool verifyuserName(string userName)
        {
                using (IDbConnection connection = new MySqlConnection(getConnectionString()))
                {
                    connection.Open();
                    List<string> userNames = connection.Query<string>($"SELECT userName FROM userInfo WHERE userName = '{userName}'").ToList();
                    if (userNames.Count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            
        }

        public bool AddSavedLocation(string city, string country, int userID)
        {
            int highestID = returnHighestID();
            if (highestID < userID || userID < 0)
            {
                return false;
            }
            using (IDbConnection connection = new MySqlConnection(getConnectionString()))
            {
                
                connection.Open();
                List<string> stateCode = connection.Query<string>("SELECT savedStateCode FROM `userSavedLocation` WHERE `savedStateCode` = @SavedStateCode AND `savedCountry` = @SavedCountry AND `userID` = @UserID", new { SavedStateCode = city, SavedCountry = country, UserID = userID }).ToList();

                if (stateCode.Count > 0)
                {
                    return false;
                }


                connection.Execute("INSERT INTO `userSavedLocation` (`savedStateCode`, `savedCountry`, `userID`) VALUES (@SavedStateCode, @SavedCountry, @UserID)", new { SavedStateCode = city, SavedCountry = country, UserID = userID });
                return true;
            }
        }


        public int returnHighestID()
        {
            using (IDbConnection connection = new MySqlConnection(getConnectionString()))
            {

                connection.Open();
                int HighestID = connection.QueryFirstOrDefault<int>($"SELECT MAX(userID) FROM userInfo");

                return HighestID;
            }
        }

        private static string getConnectionString()
        {
            string connectionString = "Server=localhost;Database=weatherappuserdata;Uid=Thomas;Pwd=1234.weather;";

            return connectionString;
        }
    }
}