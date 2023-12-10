using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Web;
using System.Web.Hosting;
using System.Web.UI.WebControls;
using WeatherApp.Properties;

namespace WeatherApp {
    public static class WeatherMan {
        /*
         * Number of the locations in the API response (up to 5 results can be returned in the API response)
         */
        private static int limit = 1;
        private static Settings settings = new Settings();

        // make me private later
        public static string ConvertLocationToCoordinates(string cityName, string stateCode, string countryCode) {
            // http://api.openweathermap.org/geo/1.0/direct?q={city name},{state code},{country code}&limit={limit}&appid={API key}
            string jsonData;
            using (var client = new HttpClient()) {
                var endpoint = new Uri($"http://api.openweathermap.org/geo/1.0/direct?q={cityName},{stateCode},{countryCode}&limit={limit}&appid={settings.APIKEY}");
                var result = client.GetAsync(endpoint).Result;
                jsonData = result.Content.ReadAsStringAsync().Result;
            }

            var parsedData = JArray.Parse(jsonData);

            
            var lat = parsedData[0]["lat"];
            var lon = parsedData[0]["lon"];

            return $"Lat: {lat}, Lon: {lon}";
        }

        public static JObject GetHourlyWeather(string cityName, string countryCode = null, string stateCode = null) {
            /*
                https://api.openweathermap.org/data/2.5/weather?q={city name}&appid={API key}
                https://api.openweathermap.org/data/2.5/weather?q={city name},{country code}&appid={API key}
                https://api.openweathermap.org/data/2.5/weather?q={city name},{state code},{country code}&appid={API key}
             */
            string url = "https://api.openweathermap.org/data/2.5/weather?q=";
            url += cityName;
            if (!string.IsNullOrEmpty(stateCode)) {
                url += $",{stateCode}";
            }
            if (!string.IsNullOrEmpty(countryCode)) {
                url += $",{countryCode}";
            }
            url += $"&appid={settings.APIKEY}";

            // Send request
            HttpResponseMessage responseMessage;
            using (var client = new HttpClient()) {
                var endPoint = new Uri(url);
                responseMessage = client.GetAsync(endPoint).Result;
            }
            var jsonData = JObject.Parse(responseMessage.Content.ReadAsStringAsync().Result);

            return jsonData;
        }
    }
}