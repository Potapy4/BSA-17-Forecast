using AutoMapper;
using System.Collections.Generic;
using System.Web.Mvc;
using WebForecast.BLL.DTO;
using WebForecast.BLL.Interfaces;
using WebForecastMVC.Models;

namespace WebForecastMVC.Controllers
{
    public class FavoriteCitiesController : Controller
    {
        private IBusinessLogic logic;

        public FavoriteCitiesController(IBusinessLogic logic)
        {
            this.logic = logic;
        }

        // GET: FavoriteCitiesController/Index
        public ActionResult Index()
        {
            IEnumerable<CityDTO> citiesDtos = logic.GetFavoriteCities();
            Mapper.Initialize(cfg => cfg.CreateMap<CityDTO, CityViewModel>());
            var cityList = Mapper.Map<IEnumerable<CityDTO>, List<CityViewModel>>(citiesDtos);

            return View(cityList);
        }

        // GET: FavoriteCitiesController/AddToFavorite
        public ActionResult AddToFavorite(string city)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                return RedirectToAction("Index");
            }           

            logic.AddFavoriteCity(city);

            return RedirectToAction("Index");
        }

        // GET: FavoriteCitiesController/Edit
        public ActionResult Edit(int id)
        {
            CityDTO cityDTO = logic.GetFavoriteCityById(id);
            Mapper.Initialize(cfg => cfg.CreateMap<CityDTO, CityViewModel>());
            var city = Mapper.Map<CityDTO, CityViewModel>(cityDTO);
            
            return View(city);
        }

        // POST: FavoriteCitiesController/Edit
        [HttpPost]
        public ActionResult Edit(CityViewModel c)
        {           
            Mapper.Initialize(cfg => cfg.CreateMap<CityViewModel, CityDTO>());
            var city = Mapper.Map<CityViewModel, CityDTO>(c);

            logic.EditFavoriteCity(city);

            return RedirectToAction("Index");
        }

        // GET: FavoriteCitiesController/Remove
        public ActionResult Remove(int id)
        {
            logic.DeleteFavoriteCity(id);

            return RedirectToAction("Index");
        }
    }
}