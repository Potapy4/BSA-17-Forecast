using System.Collections.Generic;
using System.Linq;
using WebForecastMVC.Models.Weather;

namespace WebForecastMVC.DataBase.Repositories
{
    public class FavoriteCitiesRepository : IRepository<City>
    {
        private DBModel db;

        public FavoriteCitiesRepository(DBModel db)
        {
            this.db = db;
        }

        public void Create(City item)
        {
            City c = db.FavoriteCities.FirstOrDefault(x=>x.Name == item.Name);
            if (c == null)
            {
                db.FavoriteCities.Add(item);
            }
        }

        public void Delete(int id)
        {
            City c = db.FavoriteCities.Find(id);
            if (c != null)
            {
                db.FavoriteCities.Remove(c);
            }
        }

        public City Get(int id)
        {
            return db.FavoriteCities.Find(id);
        }

        public IEnumerable<City> GetAll()
        {
            return db.FavoriteCities;
        }

        public void Update(City item)
        {
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }
    }
}