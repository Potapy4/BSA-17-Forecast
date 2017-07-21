using AutoMapper;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
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
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Id can't be negative!"));
            }

            CityDTO citiesDtos = logic.GetFavoriteCityById(id);
            Mapper.Initialize(cfg => cfg.CreateMap<CityDTO, CityViewModel>());
            var city = Mapper.Map<CityDTO, CityViewModel>(citiesDtos);

            if (city == null)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, "City not found!"));
            }

            return city;
        }

        // POST api/cities
        [HttpPost]
        public StatusCodeResult Post([FromBody]string city)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "City name can't be empty!"));
            }

            logic.AddFavoriteCity(city);

            return new StatusCodeResult(HttpStatusCode.Created, this);
        }

        // PUT api/cities/5
        [HttpPut]
        public StatusCodeResult Put([FromBody]CityViewModel ct)
        {
            if (ct == null)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "City can't be null!"));
            }

            Mapper.Initialize(cfg => cfg.CreateMap<CityViewModel, CityDTO>());
            var city = Mapper.Map<CityViewModel, CityDTO>(ct);

            logic.EditFavoriteCity(city);

            return new StatusCodeResult(HttpStatusCode.OK, this);
        }

        // DELETE api/cities/5
        [HttpDelete]
        public StatusCodeResult Delete(int id)
        {
            if (id <= 0)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Id can't be negative!"));
            }

            CityViewModel ct = Get(id); // Trying to find

            logic.DeleteFavoriteCity(id);

            return new StatusCodeResult(HttpStatusCode.OK, this);
        }
    }
}