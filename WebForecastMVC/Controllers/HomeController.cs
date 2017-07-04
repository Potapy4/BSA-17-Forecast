using System.Web.Mvc;
using WebForecastMVC.Models.Weather;
using WebForecastMVC.Services;

namespace WebForecastMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly string[] cities = new string[] { "Kiev", "Lviv", "Kharkiv", "Dnipropetrovsk", "Odessa" };

        // GET: Home/Index
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Cities = cities;
            return View();
        }       

        // GET: Home/GetForecast
        [HttpGet]
        public ActionResult GetForecast (string city, int days = 7)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                return RedirectToAction("Index");
            }

            if(days < 1 || days > 7)
            {
                return RedirectToAction("Index");
            }

            Weather wr = new ForecastProvider(city).GetForecast();
            ViewBag.Cities = cities;
            ViewBag.Days = days;

            return View("Index", wr);
        }

    }
}