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

            int userID = 201;

            //List<string> historyLocations = accountService.GetHistoryLocations(userID);
            //foreach (string location in historyLocations)
            //{
            //    ddlHistoryLocations.Items.Add(location);
            //}

            //// Get saved locations and manually add them to the dropdown list
            //List<string> savedLocations = accountService.GetSavedLocations(userID);
            //foreach (string location in savedLocations)
            //{
            //    ddlSavedLocations.Items.Add(location);
            //}
        }
    }
}