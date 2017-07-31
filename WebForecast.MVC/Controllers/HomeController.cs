using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebForecast.BLL.DTO;
using WebForecast.BLL.Interfaces;
using WebForecastMVC.Models;

namespace WebForecastMVC.Controllers
{
    public class HomeController : Controller
    {
        private IBusinessLogic logic;

        public HomeController(IBusinessLogic logic)
        {
            this.logic = logic;
        }

        // GET: Home/Index
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            IEnumerable<CityDTO> citiesDtos = await logic.GetFavoriteCitiesAsync();
            Mapper.Initialize(cfg => cfg.CreateMap<CityDTO, CityViewModel>());
            var cityList = Mapper.Map<IEnumerable<CityDTO>, List<CityViewModel>>(citiesDtos);

            ViewBag.Cities = cityList;
            return View();
        }

        // POST: Home/Index
        [HttpPost]
        public ActionResult Index(ForecastParams p)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index", "Forecast", new { city = p.City, days = p.Days });
        }
    }
}