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
        protected void Page_Load(object sender, EventArgs e)
        {
            string apiKey = settings.APIKEY;
            Weather weatherDetails = WeatherWizard.GetCurrentWeatherToWeatherObject("Bangalore");
            double latitude = weatherDetails.coord.lat;
            double longitude = weatherDetails.coord.lon;
            string script = "initMap();";
            ScriptManager.RegisterStartupScript(this, GetType(), "InitializeMap", script, true);
        }
    }
}