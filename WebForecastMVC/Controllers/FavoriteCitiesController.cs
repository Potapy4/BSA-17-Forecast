using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebForecastMVC.DataBase;
using WebForecastMVC.Models.Weather;

namespace WebForecastMVC.Controllers
{
    public class FavoriteCitiesController : Controller
    {
        // GET: FavoriteCitiesController/Index
        public ActionResult Index()
        {
            DBModel db = new DBModel();
            return View(db.FavoriteCities.ToList());
        }

        // GET: FavoriteCitiesController/AddToFavorite
        public ActionResult AddToFavorite(string city)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                return RedirectToAction("Index");
            }

            DBModel db = new DBModel();
            City ct = db.FavoriteCities.FirstOrDefault(x => x.Name == city);

            if (ct == null) // If not exists
            {
                db.FavoriteCities.Add(new City { Name = city });
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // GET: FavoriteCitiesController/Edit
        public ActionResult Edit(int? id)
        {
            DBModel db = new DBModel();
            City ct = db.FavoriteCities.FirstOrDefault(x => x.Id == id);

            return View(ct);
        }

        // POST: FavoriteCitiesController/Edit
        [HttpPost]
        public ActionResult Edit(City c)
        {
            DBModel db = new DBModel();
            City ct = db.FavoriteCities.FirstOrDefault(x => x.Id == c.Id);

            if (ct != null)
            {
                ct.Name = c.Name; // Can change only name
                db.Entry(ct).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // GET: FavoriteCitiesController/Remove
        public ActionResult Remove(int? id)
        {
            DBModel db = new DBModel();
            City ct = db.FavoriteCities.First(x => x.Id == id);

            if (ct != null)
            {
                db.FavoriteCities.Remove(ct);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}