using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WeatherApp {
    public partial class _5DayWeather : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            page_main.InnerHtml = WeatherWriter.Create5DayWeatherCard(WeatherWizard.Get5DayWeatherToJObject("Waterloo"));
        }
    }
}