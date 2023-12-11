﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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

            Assert.IsNotNull(data);
        }
    }
}