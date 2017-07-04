using System.Collections.Generic;

namespace WebForecastMVC.Models.Weather
{
    public class Weather
    {
        public City city { get; set; }
        public string cod { get; set; }
        public double message { get; set; }
        public int cnt { get; set; }
        public List<WeatherDetails> list { get; set; }
    }
}