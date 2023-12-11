using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeatherApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WeatherApp.Tests {
    [TestClass()]
    public class WeatherWizardTests {
        static string logFile = "test_outputs/logs.txt";
        public void WriteToLog(string funcName, string data) {
            using (StreamWriter sw = new StreamWriter(logFile, true)) {
                sw.WriteLine($"{DateTime.Now.ToString()} {funcName}\n{data}\n\n");
            }
        }

        [TestMethod()]
        public void GetHourlyWeatherTestWithAllParameters() {
            var data = WeatherWizard.GetHourlyWeatherToJBject("Waterloo", "CA", "ON");

            WriteToLog("GetHourlyWeatherTestWithAllParameters", data.ToString());

            Assert.IsNotNull(data);
        }

        [TestMethod()]
        public void GetHourlyWeatherTestWithTwoParameters() {
            var data = WeatherWizard.GetHourlyWeatherToJBject("Waterloo", "CA");

            WriteToLog("GetHourlyWeatherTestWithTwoParameters", data.ToString());

            Assert.IsNotNull(data);
        }

        [TestMethod()]
        public void GetHourlyWeatherTestWithOneParameters() {
            var data = WeatherWizard.GetHourlyWeatherToJBject("Waterloo");

            WriteToLog("GetHourlyWeatherTestWithOneParameters", data.ToString());

            Assert.IsNotNull(data);
        }

        [TestMethod()]
        public void GetHourlyWeatherToWeatherTest() {
            var data = WeatherWizard.GetHourlyWeatherToWeatherObject("Waterloo");
            
            // Check coord data set properly
            Assert.IsNotNull(data.coord.lat);
            Assert.IsNotNull(data.coord.lon);

            // Check weather data set properly
            foreach (var weather_data in data.weather) {
                Assert.IsNotNull(weather_data.id);
                Assert.IsNotNull(weather_data.main);
                Assert.IsNotNull(weather_data.description);
                Assert.IsNotNull(weather_data.icon);
            }

            // Check main data set properly
            Assert.IsNotNull(data.main.temp);
            Assert.IsNotNull(data.main.feels_like);
            Assert.IsNotNull(data.main.temp_min);
            Assert.IsNotNull(data.main.temp_max);
            Assert.IsNotNull(data.main.pressure);
            Assert.IsNotNull(data.main.humidity);

            // Check visibility
            Assert.IsNotNull(data.visibility);

            // Check wind data set properly
            Assert.IsNotNull(data.wind.speed);
            Assert.IsNotNull(data.wind.deg);

            // Check clouds data set properly
            Assert.IsNotNull(data.clouds.all);

            // Check datetime set properly
            Assert.IsNotNull(data.dt);

            // Check sys was set properly
            Assert.IsNotNull(data.sys.type);
            Assert.IsNotNull(data.sys.id);
            Assert.IsNotNull(data.sys.country);
            Assert.IsNotNull(data.sys.sunrise);
            Assert.IsNotNull(data.sys.sunset);

            // Check remaining
            Assert.IsNotNull(data.timezone);
            Assert.IsNotNull(data.id);
            Assert.IsNotNull(data.name);
            Assert.IsNotNull(data.cod);
        }
    }
}