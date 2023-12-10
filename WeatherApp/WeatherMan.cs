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
         * Some api calls require a limit to be provided to limit the amount of data sent back
         * Number of the locations in the API response (up to 5 results can be returned in the API response)
         */
        private static int limit = 1;
        private static Settings settings = new Settings();

        /*
         * Method       : GetHourlyWeather()
         * 
         * Description  : Takes the city name, country code (optionl), and state code (optional) then requests
         *                  the weather data from the open weather api before converting the data into a JObject
         *                  and returning the converted data.
         * 
         * Parameters   : string cityName       : The name of the city the user would like the weather info from
         *                string countryCode    : The country the city is in (optional and just used to narrow down the location)
         *                string stateCode      : The state the city is in (optional and just used to narrow down the location)
         * 
         * Returns      : JObject jsonData  : The converted data returned from the api
         *                null              : if something went wrong
         */
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

            JObject jsonData = null;
            try {
                // Send request
                HttpResponseMessage responseMessage;
                using (var client = new HttpClient()) {
                    var endPoint = new Uri(url);
                    responseMessage = client.GetAsync(endPoint).Result;
                }
                jsonData = JObject.Parse(responseMessage.Content.ReadAsStringAsync().Result);
            } catch (Exception) {
                return null;
            }
            
            return jsonData;
        }
    }
}