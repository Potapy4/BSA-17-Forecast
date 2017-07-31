using System.Collections.Generic;
using System.Threading.Tasks;
using WebForecast.BLL.BusinessModels.OpenWeatherMap;
using WebForecast.BLL.DTO;

namespace WebForecast.BLL.Interfaces
{
    public interface IBusinessLogic
    {
        Task<Weather> GetForecastAsync(string city, int? days);
        IEnumerable<CityDTO> GetFavoriteCities();
        CityDTO GetFavoriteCityById(int id);

        Task AddFavoriteCityAsync(string name);
        Task EditFavoriteCityAsync(CityDTO city);
        Task DeleteFavoriteCityAsync(int id);

        Task LogIntoHistoryAsync(HistoryDTO history);
        IEnumerable<HistoryDTO> GetAllHistory();
    }
}
