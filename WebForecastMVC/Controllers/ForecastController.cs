using System;
using System.Web.Mvc;
using WebForecastMVC.DataBase;
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

            if (wr == null)
            {
                return View("Error");
            }

            LogInDb(wr);
            return View(wr);
        }

        private void LogInDb(Weather wr)
        {
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(wr.List[0].Dt);
            DBModel db = new DBModel();

            db.History.Add(new Models.History
            {
                City = wr.City.Name,
                LogTime = DateTime.Now,
                ForecastDate = dt,
                TempMin = wr.List[0].Temp.Min,
                TempMax = wr.List[0].Temp.Max,
                Wind = wr.List[0].Speed,
                Humidity = wr.List[0].Humidity,
                Summary = wr.List[0].Weather[0].Description
            });
            db.SaveChanges();
        }
    }
}