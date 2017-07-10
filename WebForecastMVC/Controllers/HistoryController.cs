using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebForecastMVC.DataBase;

namespace WebForecastMVC.Controllers
{
    public class HistoryController : Controller
    {
        // GET: History
        public ActionResult Index()
        {
            DBModel db = new DBModel();
            return View(db.History.ToList());
        }
    }
}