using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherApp {
    public class Weather {
        public _Coord coord {  get; set; }
        public _Weather[] weather { get; set; }
    }

    public class _Coord { 
        public double lon {  get; set; }
        public double lat { get; set; }
    }

    public class _Weather {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }        
}