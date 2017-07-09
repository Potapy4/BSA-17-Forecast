using WebForecastMVC.Models.Weather;

namespace WebForecastMVC.Services
{
    public interface IForecastProvider
    {
        Weather GetForecast(string city, int? days);
    }
}
