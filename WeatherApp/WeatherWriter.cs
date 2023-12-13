using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherApp {
    public static class WeatherWriter {
        /*
         * Method       : CreateWeatherCard()
         * 
         * Description  : Generates a string that contains html elements that combined with
         *                  css styles the data into a weather card
         * 
         * Parameters   : JObject weather   : A single weather object for the card to be created from
         * 
         * Returns      : string card   : the string containing the formatted html elements with weather data
         *                null          : if a bad parameter is passed
         */
        public static string CreateCurrentWeatherCard(JObject weatherData) {
            string card = "";

            if (weatherData == null) {
                return null;
            }

            // Stored in object
            string cityName;

            // Stored in weather array as object
            string weatherDescription;
            string weatherIcon;

            // Stored in main object
            string tempCelcius;
            string humidity;

            // Stored in wind object
            string windSpeed;

            // We can source any image for these
            string windIcon = "images/wind.png";
            string humidityIcon = "images/humidity.png";

            // Current date
            string dateTime = DateTime.Now.ToString();

            // Set above data using weatherData object
            JToken token;
            JArray arr;

            if (!weatherData.TryGetValue("name", out token)) { return null; }
            cityName = token.ToString();

            // Set weather description
            if (!weatherData.TryGetValue("weather", out token)) { return null; } // Get weather value
            arr = token.ToObject<JArray>(); // Convert to JArray
            weatherDescription = arr.First.Value<string>("description"); // Only contains one object so pull out description from first
            if (weatherDescription == null) { return null; }
            weatherIcon = $"https://openweathermap.org/img/wn/{arr.First["icon"].ToString()}@2x.png";

            // Get tempurature
            if (!weatherData.TryGetValue("main", out token)) { return null; }
            tempCelcius = String.Format("{0:0.0}", ((double)token["temp"] - 273.15)); // Convert to celcius from kelvin
            humidity = token["humidity"].ToString();

            if (!weatherData.TryGetValue("wind", out token)) { return null; }
            windSpeed = token["speed"].ToString(); // meters per second

            card += "" +
                "<div class=\"weather_card\">" +
                   $"<h2 class=\"weather_card_location\">{cityName}</h2>" +
                   $"<p class=\"weather_card_description\">{dateTime}, {weatherDescription}</p>" +
                    "" +
                    "<div class=\"weather_card_temperature\">" +
                       $"<h1>{tempCelcius}<sup>&deg;C</sup></h1>" +
                       $"<img src = \"{weatherIcon}\" />" +
                    "</div>" +
                    "" +
                    "<div class=\"weather_card_extras\">" +
                        "<div class=\"weather_card_precipitation\">" +
                           $"<img src = \"{humidityIcon}\" />" +
                           $"<p> {humidity}%</p>" +
                           "<p>Humidity</p>" +
                        "</div>" +
                        "" +
                        "<div class=\"weather_card_wind\">" +
                           $"<img src = \"{windIcon}\" />" +
                           $"<p> {windSpeed} m/s</p>" +
                           "<p>Wind Speed</p>" +
                        "</div>" +
                    "</div>" +
                "</div>";

            return card;
        }
    }
}