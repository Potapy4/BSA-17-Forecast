using System.ComponentModel.DataAnnotations.Schema;

namespace WebForecastMVC.Models.Weather
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [NotMapped]
        public Coord Coord { get; set; }
        [NotMapped]
        public string Country { get; set; }
        [NotMapped]
        public int Population { get; set; }
    }
}