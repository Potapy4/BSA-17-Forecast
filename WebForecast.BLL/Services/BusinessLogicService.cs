using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        public async Task AddFavoriteCityAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("Name can't be null!");
            }

            await Database.FavoriteCities.CreateAsync(new DAL.Entities.City()
            {
                Name = name
            }).ConfigureAwait(false);
            await Database.SaveAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<CityDTO>> GetFavoriteCitiesAsync()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<DAL.Entities.City, CityDTO>());
            return Mapper.Map<IEnumerable<DAL.Entities.City>, List<CityDTO>>(await Database.FavoriteCities.GetAllAsync());
        }

        public async Task<CityDTO> GetFavoriteCityByIdAsync(int id)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<DAL.Entities.City, CityDTO>());
            return Mapper.Map<DAL.Entities.City, CityDTO>(await Database.FavoriteCities.GetAsync(id));
        }

        public async Task<Weather> GetForecastAsync(string city, int? days)
        {
            return await ForecastProvider.GetForecast(city, days).ConfigureAwait(false);
        }

        public async Task LogIntoHistoryAsync(HistoryDTO history)
        {
            if (history == null)
            {
                throw new ArgumentNullException("History can't be null!");
            }

            Mapper.Initialize(cfg => cfg.CreateMap<HistoryDTO, DAL.Entities.History>());
            DAL.Entities.History hs = Mapper.Map<HistoryDTO, DAL.Entities.History>(history);

            await Database.History.CreateAsync(hs).ConfigureAwait(false);
            await Database.SaveAsync().ConfigureAwait(false);
        }

        public async Task EditFavoriteCityAsync(CityDTO city)
        {
            if (city == null)
            {
                throw new ArgumentNullException("City can't be null!");
            }

            Mapper.Initialize(cfg => cfg.CreateMap<CityDTO, DAL.Entities.City>());
            DAL.Entities.City ct = Mapper.Map<CityDTO, DAL.Entities.City>(city);

            Database.FavoriteCities.Update(ct);
            await Database.SaveAsync().ConfigureAwait(false);

        }

        public async Task DeleteFavoriteCityAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Id can't be negative!");
            }

            await Database.FavoriteCities.DeleteAsync(id);
            await Database.SaveAsync();
        }

        public async Task<IEnumerable<HistoryDTO>> GetAllHistoryAsync()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<DAL.Entities.History, HistoryDTO>());
            return Mapper.Map<IEnumerable<DAL.Entities.History>, List<HistoryDTO>>(await Database.History.GetAllAsync());
        }
    }
}
