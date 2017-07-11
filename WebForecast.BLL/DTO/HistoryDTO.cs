using System;
using WebForecast.DAL.Entities;

namespace WebForecast.BLL.DTO
{
    public class HistoryDTO
    {
        public int Id { get; set; }
        public City City { get; set; }
        public DateTime LogTime { get; set; }
        public DateTime ForecastDate { get; set; }
        public double TempMin { get; set; }
        public double TempMax { get; set; }
        public double Wind { get; set; }
        public double Humidity { get; set; }
        public string Summary { get; set; }
    }
}
