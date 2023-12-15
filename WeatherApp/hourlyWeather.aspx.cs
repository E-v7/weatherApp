using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WeatherApp
{
    public partial class hourlyWeather : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var div = testing;
            var currentWeather = WeatherWizard.GetCurrentWeatherToJObject("waterloo");
            div.InnerHtml = WeatherWriter.CreateCurrentWeatherCard(currentWeather);
        }
    }
}