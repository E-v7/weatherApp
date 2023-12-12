using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeatherApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json.Linq;

namespace WeatherApp.Tests {
    [TestClass()]
    public class WeatherWizardTests {
        [TestMethod()]
        public void GetHourlyWeatherTestWithAllParameters() {
            var data = WeatherWizard.GetCurrentWeatherToJObject("Waterloo", "CA", "ON");

            Assert.IsNotNull(data);
        }

        [TestMethod()]
        public void GetHourlyWeatherTestWithTwoParameters() {
            var data = WeatherWizard.GetCurrentWeatherToJObject("Waterloo", "CA");

            Assert.IsNotNull(data);
        }

        [TestMethod()]
        public void GetHourlyWeatherTestWithOneParameters() {
            var data = WeatherWizard.GetCurrentWeatherToJObject("Waterloo");

            Assert.IsNotNull(data);
        }
        [TestMethod]
        public void GetHourlyWeatherTest_WithBadParameter() {
            var data = WeatherWizard.GetCurrentWeatherToJObject("bad request");

            Assert.IsNull(data);
        }


        [TestMethod()]
        public void GetHourlyWeatherToWeatherTest() {
            Weather data = WeatherWizard.GetCurrentWeatherToWeatherObject("Waterloo");

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

        [TestMethod]
        public void GetHourlyWeatherToWeather_WithBadCity() {
            Weather data = WeatherWizard.GetCurrentWeatherToWeatherObject("moon landing was fake");

            Assert.IsNull(data);
        }
        [TestMethod]
        public void GetHourlyWeatherToWeather_WithBadCityAndCountry() {
            Weather data = WeatherWizard.GetCurrentWeatherToWeatherObject("moon landing was fake", "mars");

            Assert.IsNull(data);
        }
        [TestMethod]
        public void GetHourlyWeatherToWeather_WithBadCityAndCountryAndState() {
            Weather data = WeatherWizard.GetCurrentWeatherToWeatherObject("moon landing was fake", "mars", "jupiter");

            Assert.IsNull(data);
        }

        [TestMethod]
        public void JObjectTesting() {
            var data = WeatherWizard.GetCurrentWeatherToJObject("Waterloo");

            Assert.AreEqual("Waterloo".ToLower(), data.GetValue("name").ToString().ToLower());
            Assert.IsNotNull(data.GetValue("weather").ToString());
            var test = data.GetValue("weather");
            Assert.IsInstanceOfType(test, typeof(JArray));
        }
    }
}