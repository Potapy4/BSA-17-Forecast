using System;

namespace WebForecastMVC.Models
{
    public class History
    {
        public int Id { get; set; }
        public string City { get; set; }
        public DateTime LogTime { get; set; }
        public DateTime ForecastDate { get; set; }
        public double TempMin { get; set; }
        public double TempMax { get; set; }
        public double Wind { get; set; }
        public double Humidity { get; set; }
        public string Summary { get; set; }
    }
}