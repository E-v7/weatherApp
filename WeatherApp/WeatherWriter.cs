﻿using Newtonsoft.Json.Converters;
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
            const string badData = "Something went wrong and the data provided couldn't be used";
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

            if (!weatherData.TryGetValue("name", out token)) { return badData; }
            cityName = token.ToString();

            // Set weather description
            if (!weatherData.TryGetValue("weather", out token)) { return badData; } // Get weather value
            arr = token.ToObject<JArray>(); // Convert to JArray
            weatherDescription = arr.First.Value<string>("description"); // Only contains one object so pull out description from first
            if (weatherDescription == null) { return null; }
            weatherIcon = $"https://openweathermap.org/img/wn/{arr.First["icon"].ToString()}@2x.png";

            // Get tempurature
            if (!weatherData.TryGetValue("main", out token)) { return badData; }
            tempCelcius = String.Format("{0:0.0}", ((double)token["temp"] - 273.15)); // Convert to celcius from kelvin
            humidity = token["humidity"].ToString();

            if (!weatherData.TryGetValue("wind", out token)) { return badData; }
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

        /*
         * Method       : Create5DayWeatherCard()
         * 
         * Description  : Generates a string that contains html elements that combined with
         *                  css, will be styled into a nicely formatted display of weather info
         * 
         * Parameters   : JObject weatherData - The json object containing all weather data
         * 
         * Returns      : string formattedData  : The string containing the html read to be displayed on the webpage
         *                null                  : If any issue is encountered
         */
        public static string Create5DayWeatherCard(JObject weatherData) {
            string formattedData = "";

            string windIcon = "images/wind.png";
            string humidityIcon = "images/humidity.png";

            JArray weatherArr = (JArray)weatherData["list"];
            DateTime date = DateTime.Today;
            formattedData += "<div class=fiveday_day>";
            foreach (JObject weather in weatherArr) {
                if (date.Date != DateTime.Parse((string)weather.SelectToken("$.dt_txt")).Date) {
                    formattedData += "</div><div class=fiveday_day>";
                    date = DateTime.Parse((string)weather.SelectToken("$.dt_txt"));
                }

                formattedData += "" +
                "<div class=\"weather_card\">" +
                   $"<h2 class=\"weather_card_location\">{(string)weatherData.SelectToken("$.city.name")}</h2>" +
                   $"<p class=\"weather_card_description\">{(string)weather.SelectToken("$.dt_txt")}, {(string)weather.SelectToken("$.weather[0].description")}</p>" +
                    "" +
                    "<div class=\"weather_card_temperature\">" +
                       $"<h1>{String.Format("{0:0.0}", ((double)weather.SelectToken("$.main.temp") - 273.15))}<sup>&deg;C</sup></h1>" +
                       $"<img src = \"{$"https://openweathermap.org/img/wn/{(string)weather.SelectToken("$.weather[0].icon")}@2x.png"}\" />" +
                    "</div>" +
                    "" +
                    "<div class=\"weather_card_extras\">" +
                        "<div class=\"weather_card_precipitation\">" +
                           $"<img src = \"{humidityIcon}\" />" +
                           $"<p> {(string)weather.SelectToken("$.main.humidity")}%</p>" +
                           "<p>Humidity</p>" +
                        "</div>" +
                        "" +
                        "<div class=\"weather_card_wind\">" +
                           $"<img src = \"{windIcon}\" />" +
                           $"<p> {(string)weather.SelectToken("$.wind.speed")} m/s</p>" +
                           "<p>Wind Speed</p>" +
                        "</div>" +
                    "</div>" +
                "</div>";
            }
            formattedData += "</div>";

            return formattedData;
        }
    }
}