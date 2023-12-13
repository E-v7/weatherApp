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

            string weatherDescription;
            string weatherIcon;
            string tempCelcius;
            string humidity;
            string humidityIcon;
            string windSpeed;
            string windIcon;
            string dateTime;
            string cityName;

            // Set above data using weatherData object

            card += "" +
                "<div class=\"weather_card\">" +
                   $"<h2 class=\"weather_card_location\">{cityName}</h2>" +
                   $"<p class=\"weather_card_description\">{dateTime}, {weatherDescription}</p>" +
                    "" +
                    "<div class=\"weather_card_temperature\">" +
                       $"<h1>{tempCelcius}<sup>&deg;C</sup></h1>" +
                       $"<img src = \"images/{weatherIcon}.png\" />" +
                    "</div>" +
                    "" +
                    "<div class=\"weather_card_extras\">" +
                        "<div class=\"weather_card_precipitation\">" +
                           $"<img src = \"images/{humidityIcon}.png\" />" +
                           $"<p> {humidity}% Precp </p>" +
                        "</div>" +
                        "" +
                        "<div class=\"weather_card_wind\">" +
                           $"<img src = \"images/{windIcon}.png\" />" +
                           $"<p> {windSpeed} km/h Winds</p>" +
                        "</div>" +
                    "</div>" +
                "</div>";

            return card;
        }
    }
}