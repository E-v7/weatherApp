using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeatherApp.Properties;

namespace WeatherApp {
    public class WeatherMan {
        static WeatherMan() {
            Settings settings = new Settings();
            var tmp = settings.TestSetting;
        }
    }
}