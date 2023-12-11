using Microsoft.SqlServer.Server;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WeatherApp.Properties;

namespace WeatherApp
{
    public partial class index : System.Web.UI.Page
    {

        private static Settings settings = new Settings();
        private const string BaseUrl = "https://api.openweathermap.org/data/2.5/forecast";

        protected void GetWeather(object sender, EventArgs e)
        {
            string city = Request.Form["city"];
            string apiUrl = $"{BaseUrl}?q={city}&appid={settings.APIKEY}&units=metric";

            using (WebClient client = new WebClient())
            {
                try
                {
                    string jsonData = client.DownloadString(apiUrl);
                    DisplayWeatherData(jsonData, city);
                }
                catch (WebException ex)
                {
                    WeatherOutput.Text = "Failed to fetch weather data: " + ex.Message;
                }
            }
        }



        private void DisplayWeatherData(string jsonData, string city)
        {
            try
            {
                dynamic jsonObject = JsonConvert.DeserializeObject(jsonData);

                if (jsonObject != null && jsonObject.list != null)
                {
                    var cardsHtml = new StringBuilder();
                    DateTime currentTime = DateTime.UtcNow;

                    foreach (var forecastItem in jsonObject.list)
                    {
                        long unixTime = forecastItem.dt;
                        DateTime time = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(unixTime);
                        double temperature = forecastItem.main.temp;
                        string description = forecastItem.weather[0].description;
                        double windSpeed = forecastItem.wind.speed;
                        double percipitation = 0.0;

                        if (forecastItem.rain != null && forecastItem.rain["3h"] != null)
                        {
                            percipitation = (double)forecastItem.rain["3h"];
                        }
                        else if (forecastItem.snow != null && forecastItem.snow["3h"] != null)
                        {
                            percipitation = (double)forecastItem.snow["3h"];
                        }

                        if (time > currentTime && (time - currentTime).TotalHours <= 24)
                        {
                            cardsHtml.Append("<div class='weather_card'>");
                            cardsHtml.Append($"<h2 class='weather_card_location'>{forecastItem.name}</h2>");
                            cardsHtml.Append($"<p class='weather_card_description'>{time}, {description}</p>");
                            cardsHtml.Append("<div class='weather_card_temperature'>");
                            cardsHtml.Append($"<h1>{temperature}°C</h1>");
                            cardsHtml.Append("<img src=\"images/clear-sky-day.png\" />");
                            cardsHtml.Append("</div");
                            cardsHtml.Append("<div class=\"weather_card_extras\">");
                            cardsHtml.Append("<div class=\"weather_card_precipitation\">");
                            cardsHtml.Append("<img src=\"images/rain-day.png\" />");
                            cardsHtml.Append($"<p>{percipitation}%</p>");
                            cardsHtml.Append("</div");
                            cardsHtml.Append("<div class='weather_card_wind'>");
                            cardsHtml.Append("<img src=\"images/mist-day.png\" />");
                            cardsHtml.Append($"<p>{windSpeed} m/s</p>");
                            cardsHtml.Append("</div>");
                            cardsHtml.Append("</div>");
                            cardsHtml.Append("</div>");
                        }
                    }

                    WeatherOutput.Text = cardsHtml.ToString();
                }
                else
                {
                    WeatherOutput.Text = "<p>No weather data available.</p>";
                }
            }
            catch (Exception ex)
            {
                WeatherOutput.Text = $"<p>Error: {ex.Message}</p>";
            }
        }
        private static DateTime ConvertUnixTime(long unixTime)
        {

            return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(unixTime);
            
        }

        // refactored GetWeather in order to Unit Text isolated components 
        public class Forecast
        {
            public string Description { get; set; }
            public double Precipitation { get; set; }
            public double Temperature { get; set; }
            public DateTime Time { get; set; }
            public double WindSpeed { get; set; }
        }

        public class ForecastDataProcessor
        {
            public List<Forecast> JsonParse(string jsonData)
            {
                var forecasting = new List<Forecast>();
                dynamic jsonObject = JsonConvert.DeserializeObject(jsonData);

                if (jsonObject != null && jsonObject.list != null)
                {
                    foreach (var forecastItem in jsonObject.list)
                    {
                        var forecast = new Forecast
                        {
                            Description = forecastItem.weather[0].description,
                            //Precipitation uses new Get Precipitation method 
                            Precipitation = GetPrecipitation(forecastItem),
                            Temperature = forecastItem.main.temp,
                            Time = ConvertUnixTime(forecastItem.dt),
                            WindSpeed = forecastItem.wind.speed,


                            
                        };
                    }
                }
                return forecasting;
            }

            private double GetPrecipitation(dynamic forecastItem)
            {
                if (forecastItem.rain != null && forecastItem.rain["3h"] != null)
                {
                    return (double)forecastItem.rain["3h"];
                }
                else if (forecastItem.snow != null && forecastItem.snow["3h"] != null)
                {
                    return (double)forecastItem.snow["3h"];
                }
                return 0.0;
            }

        }

        // class separate to take the city and forecast information/objects to generate HTML
        public class WeatherCardsGenerator
        {
            public string GenerateCards(List<Forecast> forecasting, string city)
            {
                var cardsHtml = new StringBuilder();
                DateTime currentTime = DateTime.UtcNow;

                foreach (var forecast in forecasting)
                {
                    if (forecast.Time > currentTime && (forecast.Time - currentTime).TotalHours <= 24)
                    {
                        cardsHtml.Append("<div class='weather_card'>");
                        cardsHtml.Append($"<h2 class='weather_card_location'>{city}</h2>");
                        cardsHtml.Append($"<p class='weather_card_description'>{forecast.Time}, {forecast.Description}</p>");
                        cardsHtml.Append("<div class='weather_card_temperature'>");
                        cardsHtml.Append($"<h1>{forecast.Temperature}°C</h1>");
                        cardsHtml.Append("<img src=\"images/clear-sky-day.png\" />");
                        cardsHtml.Append("</div");
                        cardsHtml.Append("<div class=\"weather_card_extras\">");
                        cardsHtml.Append("<div class=\"weather_card_precipitation\">");
                        cardsHtml.Append("<img src=\"images/rain-day.png\" />");
                        cardsHtml.Append($"<p>{forecast.Precipitation}%</p>");
                        cardsHtml.Append("</div");
                        cardsHtml.Append("<div class='weather_card_wind'>");
                        cardsHtml.Append("<img src=\"images/mist-day.png\" />");
                        cardsHtml.Append($"<p>{forecast.WindSpeed} m/s</p>");
                        cardsHtml.Append("</div>");
                        cardsHtml.Append("</div>");
                        cardsHtml.Append("</div>");
                    }
                }

                return cardsHtml.ToString();
            }
        }
    }
}