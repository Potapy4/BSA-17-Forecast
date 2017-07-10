namespace WebForecastMVC.DataBase
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using WebForecastMVC.Models;
    using WebForecastMVC.Models.Weather;

    public class DBModel : DbContext
    {
        public DBModel()
            : base("name=DBModel")
        {
        }

        public virtual DbSet<City> FavoriteCities { get; set; }
        public virtual DbSet<History> History { get; set; }
    }
}