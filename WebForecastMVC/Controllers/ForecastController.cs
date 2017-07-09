using System.Web.Mvc;
using WebForecastMVC.Models.Weather;
using WebForecastMVC.Services;

namespace WebForecastMVC.Controllers
{
    public class ForecastController : Controller
    {
        private IForecastProvider provider;

        public ForecastController(IForecastProvider provider)
        {
            this.provider = provider;
        }

        // GET: Forecast
        public ActionResult Index(string city, int? days)
        {
            // No access without params
            if (city == null || days == null)
            {
                return RedirectToAction("Index", "Home");
            }

            Weather wr = provider.GetForecast(city, days);
            return wr == null ? View("Error") : View(wr);
        }
    }
}