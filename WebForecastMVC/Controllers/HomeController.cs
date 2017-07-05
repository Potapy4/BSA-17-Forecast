using Ninject;
using System.Web.Mvc;
using WebForecastMVC.Models.Weather;
using WebForecastMVC.Services;

namespace WebForecastMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly string[] cities = new string[] { "Kiev", "Lviv", "Kharkiv", "Dnipropetrovsk", "Odessa" };
        private IForecastProvider provider;

        public HomeController()
        {
            IKernel ninjectKernel = new StandardKernel();
            ninjectKernel.Bind<IForecastProvider>().To<ForecastProvider>();
            provider = ninjectKernel.Get<IForecastProvider>();
        }

        // GET: Home/Index
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Cities = cities;
            return View();
        }

        // GET: Home/GetForecast
        [HttpGet]
        public ActionResult GetForecast(string city, int days = 7)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                return RedirectToAction("Index");
            }

            if (days < 1 || days > 7)
            {
                return RedirectToAction("Index");
            }

            ViewBag.Cities = cities;
            Weather wr = provider.GetForecast(city, days);

            return wr == null ? View("Error") : View("Index", wr);
        }

    }
}