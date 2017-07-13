using System.ComponentModel.DataAnnotations;

namespace WebForecastMVC.Models
{
    public class ForecastParams
    {
        [Required]
        [Display(Name = "By city name:")]
        public string City { get; set; }

        [Display(Name = "By city list:")]
        public string CityList { get; set; }

        [Display(Name = "Forecast for:")]
        public int Days { get; set; }

        public ForecastParams()
        {
            Days = 7; // Default value
        }
    }
}