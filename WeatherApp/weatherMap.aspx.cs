using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WeatherApp
{
    public partial class weatherMap : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Weather weatherDetails = WeatherWizard.GetCurrentWeatherToWeatherObject("Bangalore");
            double latitude = weatherDetails.coord.lat;
            double longitude = weatherDetails.coord.lon

        }
    }
}