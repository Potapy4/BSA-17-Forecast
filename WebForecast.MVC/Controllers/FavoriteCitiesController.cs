using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        public async Task<ActionResult> Index()
        {
            IEnumerable<CityDTO> citiesDtos = await logic.GetFavoriteCitiesAsync();
            Mapper.Initialize(cfg => cfg.CreateMap<CityDTO, CityViewModel>());
            var cityList = Mapper.Map<IEnumerable<CityDTO>, List<CityViewModel>>(citiesDtos);

            return View(cityList);
        }

        // GET: FavoriteCitiesController/AddToFavorite
        public async Task<ActionResult> AddToFavorite(string city)
        {
            try
            {
                await logic.AddFavoriteCityAsync(city).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return View("Error", model: ex.Message);
            }

            return RedirectToAction("Index");
        }

        // GET: FavoriteCitiesController/Edit
        public async Task<ActionResult> Edit(int id)
        {
            CityDTO cityDTO = await logic.GetFavoriteCityByIdAsync(id);
            Mapper.Initialize(cfg => cfg.CreateMap<CityDTO, CityViewModel>());
            var city = Mapper.Map<CityDTO, CityViewModel>(cityDTO);

            return View(city);
        }

        // POST: FavoriteCitiesController/Edit
        [HttpPost]
        public async Task<ActionResult> Edit(CityViewModel c)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<CityViewModel, CityDTO>());
            var city = Mapper.Map<CityViewModel, CityDTO>(c);

            try
            {
                await logic.EditFavoriteCityAsync(city).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return View("Error", model: ex.Message);
            }

            return RedirectToAction("Index");
        }

        // GET: FavoriteCitiesController/Remove
        public async Task<ActionResult> Remove(int id)
        {
            try
            {
                await logic.DeleteFavoriteCityAsync(id).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return View("Error", model: ex.Message);
            }

            return RedirectToAction("Index");
        }
    }
}