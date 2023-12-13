using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeatherApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace WeatherApp.Tests {
    [TestClass()]
    public class WeatherWriterTests {
        [TestMethod()]
        public void CreateCurrentWeatherCard_GoodArguments() {
            var data = WeatherWriter.CreateCurrentWeatherCard(WeatherWizard.GetCurrentWeatherToJObject("Waterloo"));

            Assert.IsNotNull(data);
        }
        [TestMethod()]
        public void CreateCurrentWeatherCard_BadArugments() {
            JObject jObj = new JObject();
            var data = WeatherWriter.CreateCurrentWeatherCard(jObj);

            Assert.IsNull(data);
        }
    }
}