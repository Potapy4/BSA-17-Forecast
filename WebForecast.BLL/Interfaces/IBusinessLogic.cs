using System.Collections.Generic;
using WebForecast.BLL.BusinessModels.OpenWeatherMap;
using WebForecast.BLL.DTO;

namespace WebForecast.BLL.Interfaces
{
    public interface IBusinessLogic
    {
        Weather GetForecast(string city, int? days);
        IEnumerable<CityDTO> GetFavoriteCities();

        void AddFavoriteCity(string name);
        void EditFavoriteCity(CityDTO city);
        void DeleteFavoriteCity(int id);

        void LogIntoHistory(HistoryDTO history);
        IEnumerable<HistoryDTO> GetAllHistory();
    }
}
