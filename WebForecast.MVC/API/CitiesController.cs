﻿using AutoMapper;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Results;
using WebForecast.BLL.DTO;
using WebForecast.BLL.Interfaces;
using WebForecastMVC.Models;

namespace WebForecastMVC.API
{
    public class CitiesController : ApiController
    {
        private IBusinessLogic logic;

        public CitiesController(IBusinessLogic logic)
        {
            this.logic = logic;
        }

        // GET api/cities
        [HttpGet]
        public IEnumerable<CityViewModel> Get()
        {
            IEnumerable<CityDTO> citiesDtos = logic.GetFavoriteCities();
            Mapper.Initialize(cfg => cfg.CreateMap<CityDTO, CityViewModel>());
            var cityList = Mapper.Map<IEnumerable<CityDTO>, List<CityViewModel>>(citiesDtos);

            return cityList;
        }

        // GET api/cities/5
        [HttpGet]
        public CityViewModel Get(int id)
        {
            if (id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            CityDTO citiesDtos = logic.GetFavoriteCityById(id);
            Mapper.Initialize(cfg => cfg.CreateMap<CityDTO, CityViewModel>());
            var city = Mapper.Map<CityDTO, CityViewModel>(citiesDtos);

            if (city == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return city;
        }

        // POST api/cities
        [HttpPost]
        public StatusCodeResult Post([FromBody]string city)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            logic.AddFavoriteCity(city);

            return new StatusCodeResult(HttpStatusCode.Created, this);
        }

        // DELETE api/cities/5
        [HttpDelete]
        public StatusCodeResult Delete(int id)
        {
            if (id <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            CityViewModel ct = Get(id); // Trying to find

            logic.DeleteFavoriteCity(id);

            return new StatusCodeResult(HttpStatusCode.OK, this);
        }
    }
}