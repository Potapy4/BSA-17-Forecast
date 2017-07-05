using System.Collections.Generic;

namespace WebForecastMVC.Models.Weather
{
    public class WeatherDetails
    {
        public int Dt { get; set; }
        public Temperature Temp { get; set; }
        public double Pressure { get; set; }
        public int Humidity { get; set; }
        public List<WeatherInfo> Weather { get; set; }
        public double Speed { get; set; }
        public int Deg { get; set; }
        public int Clouds { get; set; }
        public double? Rain { get; set; }
    }
}