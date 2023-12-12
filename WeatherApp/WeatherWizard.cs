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
    public static class WeatherWizard {
        /*
         * Some api calls require a limit to be provided to limit the amount of data sent back
         * Number of the locations in the API response (up to 5 results can be returned in the API response)
         */
        private static Settings settings = new Settings();

        /*
         * Method       : RequestCurrentWeatherAPI()
         * 
         * Description  : Builds the URL needed to call the API using the city name, country code, and state code
         *                  then returns the response as a JObject
         *                  
         * Parameters   : string cityName       : The name of the city the user would like the weather info from
         *                string countryCode    : The country the city is in (optional and just used to narrow down the location)
         *                string stateCode      : The state the city is in (optional and just used to narrow down the location)
         * 
         * Returns      : JObject jsonData  : The response from the API as a JObject
         *                null              : If API was unable to send back data
         */
        private static JObject RequestCurrentWeatherAPI(string cityName, string countryCode = null, string stateCode = null) {
            // Build url based on arguments passed
            string url = "https://api.openweathermap.org/data/2.5/weather?q=";
            url += cityName;
            if (!string.IsNullOrEmpty(stateCode)) {
                url += $",{stateCode}";
            }
            if (!string.IsNullOrEmpty(countryCode)) {
                url += $",{countryCode}";
            }
            url += $"&appid={settings.APIKEY}";

            var jsonData = RequestAPI(url);

            return jsonData;
        }

        /*
         * Method       : RequestCurrentWeatherAPI()
         * 
         * Description  : Builds the URL needed to call the API using the latitude and longitude for the location
         * 
         * Parameters   : string lat    : The latitude to be used for the weather location
         *                string lon    : The longitude to be used for the weather location
         * 
         * Returns      : JObject jsonData  : The response from the API as a JObject
         *                null              : If API was unable to send back data
         */
        private static JObject RequestCurrentWeatherAPI(string lat, string lon) {
            string url = $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={settings.APIKEY}";

            var jsonData = RequestAPI(url);

            return jsonData;
        }

        /*
         * Method       : RequestAPI()
         * 
         * Description  : This function simply takes a url string and makes a request to the OpenWeatherAPI
         *                  before returning the response as a JObject
         * 
         * Parameters   : string url    : The complete url that the request will be made on
         * 
         * Returns      : JObject jsonData  : The response from the API as a JObject
         *                null              : If API was unable to send back data
         */
        private static JObject RequestAPI(string url) {
            JObject jsonData = null;
            try {
                // Send request
                HttpResponseMessage responseMessage;
                using (var client = new HttpClient()) {
                    var endPoint = new Uri(url);
                    responseMessage = client.GetAsync(endPoint).Result;
                }
                // Convert API response to JObject
                jsonData = JObject.Parse(responseMessage.Content.ReadAsStringAsync().Result);

                if ((int)jsonData["cod"] >= 400 && (int)jsonData["cod"] <= 499) {
                    return null;
                }
            } catch (Exception) {
                // Return null if any exception is thrown
                return null;
            }

            return jsonData;
        }

        /*
        * Method       : GetHourlyWeather()
        * 
        * Description  : Takes the city name, country code (optionl), and state code (optional) then requests
        *                  the weather data from the open weather api through the RequestCurrentWeatherAPI function
        *                  then returns the converted data.
        * 
        * Parameters   : string cityName       : The name of the city the user would like the weather info from
        *                string countryCode    : The country the city is in (optional and just used to narrow down the location)
        *                string stateCode      : The state the city is in (optional and just used to narrow down the location)
        * 
        * Returns      : JObject jsonData  : The response from the API as a JObject
        *                null              : If API was unable to send back data
        */
        public static JObject GetCurrentWeatherToJObject(string cityName, string countryCode = null, string stateCode = null) {
            var jsonData = RequestCurrentWeatherAPI(cityName, countryCode, stateCode);
            
            // Return the API response as JObject
            return jsonData;
        }

        /*
        * Method       : GetHourlyWeather()
        * 
        * Description  : Takes the latitude and longitude then requests the weather data from the open weather 
        *                   api through the RequestCurrentWeatherAPI function then returns the converted data.
        * 
        * Parameters   : string lat    : The latitude to be used for the weather location
        *               string lon    : The longitude to be used for the weather location
        * 
        * Returns      : JObject jsonData  : The response from the API as a JObject
        *                null              : If API was unable to send back data
        */
        public static JObject GetCurrentWeatherToJObject(string lat, string lon) {
            var jsonData = RequestCurrentWeatherAPI(lat, lon);

            // Return the API response as JObject
            return jsonData;
        }

        /*
         * Method       : GetHourlyWeatherToWeatherObject()
         * 
         * Description  : Takes a city name, country, and state then requests data from the API
         *                  before formatting it into the Weather object that holds the properties
         *                  of the response objects
         * 
         * Parameters   : string cityName       : The name of the city the user would like the weather info from
         *                string countryCode    : The country the city is in (optional and just used to narrow down the location)
         *                string stateCode      : The state the city is in (optional and just used to narrow down the location)
         * 
         * Returns      : Weather weather   : The newly created weather object
         *                null              : if the object couldn't be created
         */
        public static Weather GetCurrentWeatherToWeatherObject(string cityName, string countryCode = null, string stateCode = null) {
            Weather weather = null;

            var jsonData = GetCurrentWeatherToJObject(cityName, countryCode, stateCode);

            if (weather == null) {
                return null;
            }

            weather = JsonConvert.DeserializeObject<Weather>(jsonData.ToString());

            if (weather.cod >= 400 && weather.cod <= 499) {
                return null;
            }

            return weather;
        }

        /*
         * Method       : GetHourlyWeatherToWeatherObject()
         * 
         * Description  : Takes a city name, country, and state then requests data from the API
         *                  before formatting it into the Weather object that holds the properties
         *                  of the response objects
         * 
         * Parameters   : string lat    : The latitude to be used for the weather location
         *                string lon    : The longitude to be used for the weather location
         * 
         * Returns      : Weather weather   : The response from the API as a Weather object
         *                null              : If API was unable to send back data
         */
        public static Weather GetCurrentWeatherToWeatherObject(string lat, string lon) {
            Weather weather = null;

            var jsonData = RequestCurrentWeatherAPI(lat, lon);

            weather = JsonConvert.DeserializeObject<Weather>(jsonData.ToString());

            if (weather == null || weather.cod >= 400 && weather.cod <= 499) {
                return null;
            }

            return weather;
        }
    }
}