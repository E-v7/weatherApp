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
using System.Threading.Tasks;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WeatherApp {
    public partial class index : System.Web.UI.Page 
    {
        [WebMethod]
        public static string GetWeather(string lat, string lon)
        {
            // Call the WeatherWizard class method to get the current weather
            JObject weatherData = WeatherWizard.GetCurrentWeatherToJObject(lat, lon);

            // Convert the JObject to a JSON string to send back to the client
            return weatherData.ToString();
        }
    }
}