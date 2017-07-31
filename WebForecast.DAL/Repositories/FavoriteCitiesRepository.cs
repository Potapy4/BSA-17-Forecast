using System.Collections.Generic;
using System.Data.Entity;
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

        public async Task CreateAsync(City item)
        {
            City c = await db.FavoriteCities.FirstOrDefaultAsync(x => x.Name == item.Name);
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

        public async Task<City> GetAsync(int id)
        {
            return await db.FavoriteCities.FindAsync(id);
        }

        public async Task<IEnumerable<City>> GetAllAsync()
        {
            return await db.FavoriteCities.ToListAsync();
        }

        public void Update(City item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
