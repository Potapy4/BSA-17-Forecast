using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using WebForecast.DAL.Entities;
using WebForecast.DAL.EntityFramework;
using WebForecast.DAL.Interfaces;

namespace WebForecast.DAL.Repositories
{
    public class HistoryRepository: IRepository<History>
    {
        private ForecastContext db;

        public HistoryRepository(ForecastContext db)
        {
            this.db = db;
        }

        public async Task CreateAsync(History item)
        {
            await Task.Run(() => db.History.Add(item)).ConfigureAwait(false);
        }

        public async Task DeleteAsync(int id)
        {
            History hs = await db.History.FindAsync(id).ConfigureAwait(false);

            if (hs != null)
            {
                db.History.Remove(hs);
            }
        }

        public async Task<History> GetAsync(int id)
        {
            return await db.History.FindAsync(id).ConfigureAwait(false);
        }

        public async Task<IEnumerable<History>> GetAllAsync()
        {
            return await db.History.ToListAsync();
        }

        public void Update(History item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
