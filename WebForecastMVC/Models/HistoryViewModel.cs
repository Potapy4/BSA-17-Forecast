using System;
using WebForecast.BLL.DTO;

namespace WebForecastMVC.Models
{
    public class HistoryViewModel
    {
        public int Id { get; set; }
        public CityDTO City { get; set; }
        public DateTime LogTime { get; set; }
        public DateTime ForecastDate { get; set; }
        public double TempMin { get; set; }
        public double TempMax { get; set; }
        public double Wind { get; set; }
        public double Humidity { get; set; }
        public string Summary { get; set; }
    }
}