using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WeatherApp.Properties;
using static System.Net.Mime.MediaTypeNames;

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
                string script = "getLocationAndWeather('"+apiKey+"');";
                ScriptManager.RegisterStartupScript(this, GetType(), "InitializeMap", script, true);
                
            }
        }

   /*     public void GetCurrentWeather(string lat, string lon)
        {
            // call the WeatherWizard class method to get the current weather
            Weather weatherDetails = WeatherWizard.GetCurrentWeatherToWeatherObject(lat, lon);
            double latitude = weatherDetails.coord.lat;
            double longitude = weatherDetails.coord.lon;
            string description = weatherDetails.name + ": " + weatherDetails.main.temp + "°K, " + weatherDetails.weather[0].description;
            string script = "initMap(" + latitude + ", " + longitude + ", '" + apiKey + "', '" + description + "');";
            ScriptManager.RegisterStartupScript(this, GetType(), "InitializeMap", script, true);
        }*/

        protected void GoToLocation(object sender, EventArgs e)
        {
            string city = Request.Form["city"];
            //default coordinates to reset the map
            double latitude = 40.7143;
            double longitude = -74.006;
            string description = "";

            if (city != "")
            { 
                Weather weatherDetails = WeatherWizard.GetCurrentWeatherToWeatherObject(city);
                if (weatherDetails != null) {
                    latitude = weatherDetails.coord.lat;
                    longitude = weatherDetails.coord.lon;
                    description = weatherDetails.name+": "+ weatherDetails.main.temp+ "°K, " + weatherDetails.weather[0].description;
                    string script = "initMap(" + latitude + ", " + longitude + ", '" + apiKey + "', '" + description + "');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "InitializeMap", script, true);
                }
                else {
                    //if the search is invalid, alert and reset to default coordinates
                    string script = "alert('Unable to get weather for this location!');";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                    string scriptMap = "initMap("+latitude+","+longitude+", '" + apiKey + "', '" + description + "');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "InitializeMap", scriptMap, true);
                }
            }
        }
    }
}