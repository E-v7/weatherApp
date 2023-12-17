using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WeatherApp {
    public partial class _5DayWeather : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if (Session["LastSearch"] != null) {
                ParseAndDisplayWeatherData(Session["LastSearch"].ToString());
            }
        }

        protected void Search_Button_Click(object sender, EventArgs e) {
            string searchQuery = Request.Form["city"];

            ParseAndDisplayWeatherData(searchQuery);
        }

        private void ParseAndDisplayWeatherData(string searchQuery) {
            string[] searchParts = searchQuery.Split(',');

            // determines the number of parts and call the appropriate WeatherWizard method
            if (searchParts.Length > 0 && searchParts.Length < 4) {
                var weatherData = WeatherWizard.Get5DayWeatherToJObject(searchParts[0], searchParts.Length > 1 ? searchParts[1] : null, searchParts.Length > 2 ? searchParts[2] : null);
                page_main.InnerHtml = WeatherWriter.Create5DayWeatherTable(weatherData);

                Session["LastSearch"] = searchQuery;
            } else {
                page_main.InnerHtml = "<p>Something went wrong, please try searching again.</p>" +
                    "<p>Expected format: City, Country Code, State Code</p>";
            }
        }
    }
}