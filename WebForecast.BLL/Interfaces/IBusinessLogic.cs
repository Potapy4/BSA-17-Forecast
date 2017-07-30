using System.Collections.Generic;
using System.Threading.Tasks;
using WebForecast.BLL.BusinessModels.OpenWeatherMap;
using WebForecast.BLL.DTO;

namespace WebForecast.BLL.Interfaces
{
    public interface IBusinessLogic
    {
        Weather GetForecast(string city, int? days);
        IEnumerable<CityDTO> GetFavoriteCities();
        CityDTO GetFavoriteCityById(int id);

        void AddFavoriteCity(string name);
        void EditFavoriteCity(CityDTO city);
        Task DeleteFavoriteCityAsync(int id);

        void LogIntoHistory(HistoryDTO history);
        IEnumerable<HistoryDTO> GetAllHistory();
    }
}
