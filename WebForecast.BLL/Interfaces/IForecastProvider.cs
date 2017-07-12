using WebForecast.BLL.BusinessModels.OpenWeatherMap;

namespace WebForecast.BLL.Interfaces
{
    public interface IForecastProvider
    {
        Weather GetForecast(string city, int? days);
    }
}
