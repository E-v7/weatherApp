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
using WeatherApp.Properties;

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

    public class UserHistory
    {
        public string HistoryStateCode { get; set; }
        public string HistoryCountry { get; set; }
        public DateTime SearchTime { get; set; }
    }

    public class SavedLocation
    {
        public string savedStateCode { get; set; }
        public string savedCountry { get; set; }
    }


    public class AccountServicing
    {
        private readonly List<User> users = new List<User>(); // Simulated database

        private readonly string connectionString;
        // Strong password regex pattern
        private readonly Regex passwordRegex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{12,}$");

        private static Settings settings = new Settings();

        private DateTime currentDateTime = DateTime.Now;

        public AccountServicing(string connectionString)
        {
            this.connectionString = connectionString;
        }

        /*
        * Method         : CreateAccount
        * Description    : This methods will check if all information is valid and will store it in the database if it is
        * Parameters     : string userName : this holds the users name, string passWord : this holds the users password, string email : this holds the users email
        * Returns        : bool : returns true if everything went well nd return false when something is invalid
        */

        public bool CreateAccount(string username, string password, string email)
        {
            //if statements will check to mke sure all form data is correct
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
            //crete the db connection
            using (IDbConnection connection = new MySqlConnection(getConnectionString()))
            {
                //conect to the db
                connection.Open();
                //hash the user password
                string hashOfEnteredPassword = HashPassword(password);

                //start a transaction so all either all statements get run or none
                using (var transaction = connection.BeginTransaction())
                {
                    //insert the user info into the database
                    connection.Execute("INSERT INTO `userInfo` (`userName`, `userEmail`) VALUES (@Username, @Email)", new { Username = username, Email = email });

                    //get the latest userID inputted in the db
                    int userID = connection.QuerySingle<int>("SELECT LAST_INSERT_ID()");

                    //insert the password with the most resnt id
                    connection.Execute("INSERT INTO `userPassword` (`userPassword`, `userID`) VALUES (@UserPassword, @UserID)", new { UserPassword = hashOfEnteredPassword, UserID = userID });
                    //perform the transaction
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

        /*
        * Method         : VerifyLogin
        * Description    : This methods will check if login info is valid
        * Parameters     : string userName : this holds the users name entered during login, string passWord : this holds the users password entered during login
        * Returns        : bool : returns true if login found false if not found
        */
        public bool VerifyLogin(string userName, string enteredPassword)
        {
            //crete the db connection
            using (IDbConnection connection = new MySqlConnection(getConnectionString()))
            {
                //connect to the db
                connection.Open();
                //hash the password
                string hashOfEnteredPassword = HashPassword(enteredPassword);
                //select the passwords if the user id and the password are found
                List<string> userPassword = connection.Query<string>($"SELECT up.userPassword FROM userPassword AS up INNER JOIN userInfo AS ui ON up.userID = ui.userID WHERE ui.userName = '{userName}' AND userPassword = '{hashOfEnteredPassword}'").ToList();
                //check if anything was found
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

        /*
        * Method         : verifyuserName
        * Description    : This methods will check if username is valid
        * Parameters     : string userName : this holds the users name entered during login
        * Returns        : bool : returns true if login found false if not found
        */

        public bool verifyuserName(string userName)
        {
                //create the db connection
                using (IDbConnection connection = new MySqlConnection(getConnectionString()))
                {
                //connect to the db
                    connection.Open();
                //select usernames that are found
                    List<string> userNames = connection.Query<string>($"SELECT userName FROM userInfo WHERE userName = '{userName}'").ToList();
                //check if any usernames are found
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

        /*
        * Method         : AddSavedLocation
        * Description    : This methods will check if all information is valid and will store the saved location in the database
        * Parameters     : string city : this holds the city wanting to be saved, string country : this holds the country wantring to be saved, int userID : this holds the userId that wants to save the location
        * Returns        : bool : returns true if everything went well and return false when something is invalid
        */

        public bool AddSavedLocation(string city, string country, int userID)
        {
            int highestID = returnHighestID();
            if (highestID < userID || userID < 0)
            {
                return false;
            }
            //crete the db connection
            using (IDbConnection connection = new MySqlConnection(getConnectionString()))
            {
                //connect to the db
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

        /*
        * Method         : AddHistoryLocation
        * Description    : This methods will check if all information is valid and will store the history location in the database
        * Parameters     : string city : this holds the city that is going to be sved in history, string country : this holds the country to that is going to be sved in history, int userID : this holds the userId that wants to save the location
        * Returns        : bool : returns true if everything went well and return false when something is invalid
        */

        public bool AddHistoryLocation(string city, string country, int userID)
        {
            int highestID = returnHighestID();
            if (highestID < userID || userID < 0)
            {
                return false;
            }
            //crete the db connection
            using (IDbConnection connection = new MySqlConnection(getConnectionString()))
            {
                string formatedTime = currentDateTime.ToString("yyyy-MM-dd HH:mm:ss");

                connection.Execute($"INSERT INTO `userHistory` (`historyStateCode`, `historyCountry`, `searchTime`, `userID`) VALUES ('{city}', '{country}', '{formatedTime}', {userID});");
                return true;
            }
        }

        public List<string> GetHistoryLocations(int userID)
        {
            int highestID = returnHighestID();
            if (highestID < userID || userID < 0)
            {
                throw new Exception("userID out of bounds");
            }

            using (IDbConnection connection = new MySqlConnection(getConnectionString()))
            {
                //connect to the db
                connection.Open();
                List<string> history = connection.Query<string>($"SELECT historyStateCode FROM userHistory WHERE userID = {userID}").ToList();

                

                return history;
            }

        }

        public List<string> GetSavedLocations(int userID)
        {
            int highestID = returnHighestID();
            if (highestID < userID || userID < 0)
            {
                throw new Exception("userID out of bounds");
            }

            using (IDbConnection connection = new MySqlConnection(getConnectionString()))
            {
                //connect to the db
                connection.Open();
                List<string> savedLoactions = connection.Query<string>($"SELECT savedStateCode FROM userSavedLocation WHERE userID = {userID}").ToList();

                return savedLoactions;
            }

        }

        /*
        * Method         : returnHighestID
        * Description    : This methods will return the higest userID in the database
        * Parameters     : none
        * Returns        : int : return the highest userID
        */

        public int returnHighestID()
        {
            //crete the db connection
            using (IDbConnection connection = new MySqlConnection(getConnectionString()))
            {
                //connect to the db
                connection.Open();
                //search for the max userID
                int HighestID = connection.QueryFirstOrDefault<int>($"SELECT MAX(userID) FROM userInfo");
                return HighestID;
            }
        }

        /*
        * Method         : getConnectionString
        * Description    : This methods will return the connection string
        * Parameters     : none
        * Returns        : string : this will return the conneciton string
        */

        private static string getConnectionString()
        {
            //this is the connection string it has to be private but for now hardcoded
            string connectionString = settings.ConnectionString;

            return connectionString;
        }
    }


}