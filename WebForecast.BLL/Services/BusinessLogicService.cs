using AutoMapper;
using System.Collections.Generic;
using WebForecast.BLL.BusinessModels.OpenWeatherMap;
using WebForecast.BLL.BusinessModels.Services;
using WebForecast.BLL.DTO;
using WebForecast.BLL.Infrastructure;
using WebForecast.BLL.Interfaces;
using WebForecast.DAL.Interfaces;
using System;

namespace WebForecast.BLL.Services
{
   public class BusinessLogicService : IBusinessLogic
    {
        private IUnitOfWork Database { get; set; }

        public BusinessLogicService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void AddFavoriteCity(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ValidationException("Name can not be empty!", "");
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

        public Weather GetForecast(string city, int? days)
        {
            return new OpenWeatherMapProvider(Properties.Settings.Default.apiKey).GetForecast(city, days);
        }

        public void LogIntoHistory(HistoryDTO history)
        {
            if (history == null)
            {
                throw new ValidationException("History can not be null!", "");
            }

            DAL.Entities.History hs = Mapper.Map<DAL.Entities.History>(history);

            Database.History.Create(hs);
            Database.Save();
        }

        public void EditFavoriteCity(CityDTO city)
        {
            if (city == null)
            {
                throw new ValidationException("City can not be null!", "");
            }

            DAL.Entities.City ct = Mapper.Map<DAL.Entities.City>(city);

            Database.FavoriteCities.Update(ct);
            Database.Save();

        }

        public void DeleteFavoriteCity(int id)
        {
            if (id <= 0)
            {
                throw new ValidationException("Invalid id!", "");
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
