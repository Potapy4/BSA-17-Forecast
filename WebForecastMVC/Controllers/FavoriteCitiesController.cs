using System.Web.Mvc;
using WebForecastMVC.DataBase;
using WebForecastMVC.Models.Weather;

namespace WebForecastMVC.Controllers
{
    public class FavoriteCitiesController : Controller
    {
        private UnitOfWork uow;

        public FavoriteCitiesController()
        {
            uow = new UnitOfWork();
        }

        // GET: FavoriteCitiesController/Index
        public ActionResult Index()
        {
            return View(uow.FavoriteCities.GetAll());
        }

        // GET: FavoriteCitiesController/AddToFavorite
        public ActionResult AddToFavorite(string city)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                return RedirectToAction("Index");
            }

            uow.FavoriteCities.Create(new City { Name = city });
            uow.Save();

            return RedirectToAction("Index");
        }

        // GET: FavoriteCitiesController/Edit
        public ActionResult Edit(int id)
        {
            City c = uow.FavoriteCities.Get(id);
            return View(c);
        }

        // POST: FavoriteCitiesController/Edit
        [HttpPost]
        public ActionResult Edit(City c)
        {
            uow.FavoriteCities.Update(c);
            uow.Save();

            return RedirectToAction("Index");
        }

        // GET: FavoriteCitiesController/Remove
        public ActionResult Remove(int id)
        {
            uow.FavoriteCities.Delete(id);
            uow.Save();

            return RedirectToAction("Index");
        }
    }
}