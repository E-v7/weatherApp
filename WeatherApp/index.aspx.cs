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
                            cardsHtml.Append($"<h2 class='weather_card_location'>{city}</h2>");
                            cardsHtml.Append($"<p class='weather_card_description'>{time}, {description}</p>");
                            cardsHtml.Append("<div class='weather_card_temperature'>");
                            cardsHtml.Append($"<h1>{temperature}°C</h1>");
                            cardsHtml.Append("<img src=\"images/clear-sky-day.png\" />");
                            cardsHtml.Append("</div");
                            cardsHtml.Append("<div class=\"weather_card_extras\">");
                            cardsHtml.Append("<div class=\"weather_card_precipitation\">");
                            cardsHtml.Append("<img src=\"images/rain-day.png\" />");
                            cardsHtml.Append($"<p>{percipitation}</p>");
                            cardsHtml.Append("</div");
                            cardsHtml.Append("<div class='weather_card_wind'>");
                            cardsHtml.Append("<img src=\"images/mist-day.png\" />");
                            cardsHtml.Append($"<p>{windSpeed}</p>");
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
        private DateTime ConvertUnixTime(long unixTime)
        {

            DateTime time = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(unixTime);
            return time;
        }
    }
}