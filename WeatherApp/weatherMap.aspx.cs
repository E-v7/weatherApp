using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WeatherApp.Properties;

namespace WeatherApp
{
    public partial class weatherMap : System.Web.UI.Page
    {
        private static Settings settings = new Settings();
        private string apiKey = settings.APIKEY;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Weather weatherDetails = WeatherWizard.GetCurrentWeatherToWeatherObject("bangalore");
                double latitude = weatherDetails.coord.lat;
                double longitude = weatherDetails.coord.lon;
                string description = weatherDetails.weather[0].description;
                string script = "initMap(" + latitude + ", " + longitude + ", '" + apiKey + "', '" + description + "');";
                ScriptManager.RegisterStartupScript(this, GetType(), "InitializeMap", script, true);
            }
        }

        protected void GoToLocation(object sender, EventArgs e)
        {
            string city = Request.Form["city"];
            if (city != "")
            {
                Weather weatherDetails = WeatherWizard.GetCurrentWeatherToWeatherObject(city);
                double latitude = weatherDetails.coord.lat;
                double longitude = weatherDetails.coord.lon;
                string description = weatherDetails.weather[0].description;
                string script = "initMap(" + latitude + ", " + longitude + ", '" + apiKey + "', '" + description + "');";
                ScriptManager.RegisterStartupScript(this, GetType(), "InitializeMap", script, true);
            }
        }
    }
}