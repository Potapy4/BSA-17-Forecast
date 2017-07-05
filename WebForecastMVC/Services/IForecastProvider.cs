using WebForecastMVC.Models.Weather;

namespace WebForecastMVC.Services
{
    interface IForecastProvider
    {
        Weather GetForecast(string city, int days);
    }
}
