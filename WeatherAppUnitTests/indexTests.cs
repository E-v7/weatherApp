using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using WeatherApp;
using static WeatherApp.index;

namespace WeatherAppUnitTests
{
    [TestClass]
    public class indexTests
    {
        [TestMethod]
        public void JsonParse_ValidData_ReturnsCorrectForecasts()
        {
            // Arrange
            var processor = new ForecastDataProcessor();
            string validJson = "{ \"list\": [{ \"dt\": 1618308000, \"main\": { \"temp\": 15 }, \"weather\": [{ \"description\": \"clear sky\" }], \"wind\": { \"speed\": 1.5 }, \"rain\": { \"3h\": 0.0 } }] }";

            // Act
            var result = processor.JsonParse(validJson);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(1618308000), result[0].Time);
            Assert.AreEqual(15, result[0].Temperature);
            Assert.AreEqual("clear sky", result[0].Description);
            Assert.AreEqual(1.5, result[0].WindSpeed);
            Assert.AreEqual(0.0, result[0].Precipitation);

        }
    }
}

