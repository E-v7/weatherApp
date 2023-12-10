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
    public class WeatherManTests {
        static string logFile = "test_outputs/logs.txt";
        public void WriteToLog(string funcName, string data) {
            using (StreamWriter sw = new StreamWriter(logFile, true)) {
                sw.WriteLine($"{DateTime.Now.ToString()} {funcName}\n{data}\n\n");
            }
        }

        [TestMethod()]
        public void ConvertLocationToCoordinatesTest() {
            var data = WeatherMan.ConvertLocationToCoordinates("Waterloo", "ON", "CA");

            // Write data to file so it can be observed
            WriteToLog("ConvertLocationToCoordinatesTest", data);

            Assert.IsNotNull(data);
        }

        [TestMethod()]
        public void GetHourlyWeatherTestWithAllParameters() {
            var data = WeatherMan.GetHourlyWeather("Waterloo", "CA", "ON");

            WriteToLog("GetHourlyWeatherTestWithAllParameters", data.ToString());

            Assert.IsNotNull(data);
        }

        [TestMethod()]
        public void GetHourlyWeatherTestWithTwoParameters() {
            var data = WeatherMan.GetHourlyWeather("Waterloo", "CA");

            WriteToLog("GetHourlyWeatherTestWithTwoParameters", data.ToString());

            Assert.IsNotNull(data);
        }

        [TestMethod()]
        public void GetHourlyWeatherTestWithOneParameters() {
            var data = WeatherMan.GetHourlyWeather("Waterloo");

            WriteToLog("GetHourlyWeatherTestWithOneParameters", data.ToString());

            Assert.IsNotNull(data);
        }
    }
}