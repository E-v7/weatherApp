using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WeatherApp.Properties;

namespace WeatherApp
{
    public partial class Site1 : System.Web.UI.MasterPage
    {

        private readonly AccountServicing accountService = new AccountServicing(settings.ConnectionString);

        private static Settings settings = new Settings();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}