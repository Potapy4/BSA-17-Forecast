using System.Threading.Tasks;
using WebForecast.BLL.BusinessModels.OpenWeatherMap;

namespace WebForecast.BLL.Interfaces
{
    public interface IForecastProvider
    {
        Task<Weather> GetForecast(string city, int? days);
    }
}
