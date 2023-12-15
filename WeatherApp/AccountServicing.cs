using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Dapper;
using MySql.Data.MySqlClient;
using WeatherApp;

namespace WeatherApp
{
    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        // Additional properties can be added here as needed
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

        public bool CreateAccount(string firstName, string lastName, string username, string password, string email)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Username, password, and email cannot be blank.");
            }
            if (!passwordRegex.IsMatch(password))
            {
                throw new ArgumentException("Password does not meet security requirements.");
            }
            if (users.Any(u => u.Username == username || u.Email == email))
            {
                throw new ArgumentException("An account with this username or email already exists.");
            }
            var newUser = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Username = username,
                Email = email,
                PasswordHash = HashPassword(password)
            };
            users.Add(newUser);
            return true;
        }
        public bool ValidateLogin(string usernameOrEmail, string password)
        {
            var user = users.FirstOrDefault(u => u.Username == usernameOrEmail || u.Email == usernameOrEmail);
            if (user == null)
            {
                return false; // User not found
            }
            // Check if the hashed password matches
            return VerifyPassword(password, user.PasswordHash);
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
        public bool VerifyPassword(string userName, string enteredPassword)
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


        private static string getConnectionString()
        {
            string connectionString = "Server=localhost;Database=weatherappuserdata;Uid=Thomas;Pwd=1234.weather;";

            return connectionString;
        }
    }
}