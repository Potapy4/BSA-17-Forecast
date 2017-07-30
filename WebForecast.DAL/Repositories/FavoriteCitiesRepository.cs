using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebForecast.DAL.Entities;
using WebForecast.DAL.EntityFramework;
using WebForecast.DAL.Interfaces;

namespace WebForecast.DAL.Repositories
{
    public class FavoriteCitiesRepository : IRepository<City>
    {
        private ForecastContext db;

        public FavoriteCitiesRepository(ForecastContext db)
        {
            this.db = db;
        }

        public void Create(City item)
        {
            City c = db.FavoriteCities.FirstOrDefault(x => x.Name == item.Name);
            if (c == null)
            {
                db.FavoriteCities.Add(item);
            }
        }

        public async Task DeleteAsync(int id)
        {
            City c = await db.FavoriteCities.FindAsync(id).ConfigureAwait(false);
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
