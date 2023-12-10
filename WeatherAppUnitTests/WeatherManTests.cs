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
        [TestMethod()]
        public void ConvertLocationToCoordinatesTest() {
            var data = WeatherMan.ConvertLocationToCoordinates("Waterloo", "ON", "CA");

            // Write data to file so it can be observed
            using (StreamWriter sw = new StreamWriter("test_outputs/Convert_Location_To_Coordinates_Test.txt", true)) {
                sw.WriteLine($"{DateTime.Now.ToString()} {data}");
            }

            Assert.IsNotNull(data);
        }
    }
}