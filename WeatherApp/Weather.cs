using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherApp {
    /*
     * Class        : Weather
     * Description  : This class was created to be used along with the JsonConvert.DeserializeObject<T>(string) function
     *                  and when called will set all properties of this class object to the data it extracts from the
     *                  json data. It was created to make the code more readable and so the API can be called less often.
     *                  
     *                  Also this should work with the 5 day weather data as well but if it does not it can be easily modified
     *                  to do so!
     */
    public class Weather {
        public _Coord coord { get; set; }
        public _Weather[] weather { get; set; }
        // public string base { get; set; } not possible due to variable name 'base'
        public _Main main { get; set; }
        public int visibility { get; set; }
        public _Wind wind { get; set; }
        public _Clouds clouds { get; set; }
        public int dt { get; set; } // may want to change the set code here to return a readable format
        public _Sys sys { get; set; }
        public int timezone { get; set; } // unsure of what format is used here
        public int id { get; set; }
        public string name { get; set; }
        public int cod { get; set; }
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

    public class _Main {
        public double temp { get; set; }
        public double feels_like { get; set; }
        public double temp_min { get; set; }
        public double temp_max { get; set; }
        public int pressure { get; set; }
        public int humidity { get; set; }
    }

    public class _Wind {
        public double speed { get; set; }
        public int deg { get; set; }
    }

    public class _Clouds {
        public int all { get; set; }
    }

    public class _Sys {
        public int type { get; set; }
        public int id { get; set; }
        public string country { get; set; }
        public int sunrise { get; set; } // uses unix time here as well
        public int sunset { get; set; } // and here
    }
}