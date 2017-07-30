using AutoMapper;
using System;
using System.Collections.Generic;
using WebForecast.BLL.BusinessModels.OpenWeatherMap;
using WebForecast.BLL.DTO;
using WebForecast.BLL.Interfaces;
using WebForecast.DAL.Interfaces;

namespace WebForecast.BLL.Services
{
    public class BusinessLogicService : IBusinessLogic
    {
        private IUnitOfWork Database { get; set; }
        private IForecastProvider ForecastProvider { get; set; }

        public BusinessLogicService(IUnitOfWork uow, IForecastProvider provider)
        {
            Database = uow;
            ForecastProvider = provider;
        }

        public void AddFavoriteCity(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("Name can't be null!");
            }

            Database.FavoriteCities.Create(new DAL.Entities.City()
            {
                Name = name
            });
            Database.Save();
        }

        public IEnumerable<CityDTO> GetFavoriteCities()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<DAL.Entities.City, CityDTO>());
            return Mapper.Map<IEnumerable<DAL.Entities.City>, List<CityDTO>>(Database.FavoriteCities.GetAll());
        }

        public CityDTO GetFavoriteCityById(int id)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<DAL.Entities.City, CityDTO>());
            return Mapper.Map<DAL.Entities.City, CityDTO>(Database.FavoriteCities.Get(id));
        }

        public Weather GetForecast(string city, int? days)
        {
            return ForecastProvider.GetForecast(city, days);
        }

        public void LogIntoHistory(HistoryDTO history)
        {
            if (history == null)
            {
                throw new ArgumentNullException("History can't be null!");
            }

            Mapper.Initialize(cfg => cfg.CreateMap<HistoryDTO, DAL.Entities.History>());
            DAL.Entities.History hs = Mapper.Map<HistoryDTO, DAL.Entities.History>(history);

            Database.History.Create(hs);
            Database.Save();
        }

        public void EditFavoriteCity(CityDTO city)
        {
            if (city == null)
            {
                throw new ArgumentNullException("City can't be null!");
            }

            Mapper.Initialize(cfg => cfg.CreateMap<CityDTO, DAL.Entities.City>());
            DAL.Entities.City ct = Mapper.Map<CityDTO, DAL.Entities.City>(city);

            Database.FavoriteCities.Update(ct);
            Database.Save();

        }

        public void DeleteFavoriteCity(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Id can't be negative!");
            }

            Database.FavoriteCities.Delete(id);
            Database.Save();
        }

        public IEnumerable<HistoryDTO> GetAllHistory()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<DAL.Entities.History, HistoryDTO>());
            return Mapper.Map<IEnumerable<DAL.Entities.History>, List<HistoryDTO>>(Database.History.GetAll());
        }
    }
}
