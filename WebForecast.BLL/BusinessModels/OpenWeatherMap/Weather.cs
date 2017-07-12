using System.Collections.Generic;

namespace WebForecast.BLL.BusinessModels.OpenWeatherMap
{
    public class Weather
    {
        public City City { get; set; }
        public string Cod { get; set; }
        public double Message { get; set; }
        public int Cnt { get; set; }
        public List<WeatherDetails> List { get; set; }
    }
}
