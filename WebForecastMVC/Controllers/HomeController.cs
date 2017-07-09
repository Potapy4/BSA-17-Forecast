using System.Web.Mvc;
using WebForecastMVC.Models;

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

        // POST: Home/Index
        [HttpPost]
        public ActionResult Index(ForecastParams p)
        {
            return RedirectToAction("Index", "Forecast", new { city = p.City, days = p.Days });
        }
    }
}