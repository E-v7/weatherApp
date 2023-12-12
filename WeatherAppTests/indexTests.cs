using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeatherApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Tests {
    [TestClass()]
    public class indexTests {
        [TestMethod()]
        public void ConvertUnixTimeTest() {
            ConvertUnixTime(1702250463);

            Assert.Fail();
        }
    }
}