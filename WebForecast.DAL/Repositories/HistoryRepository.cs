using System.Collections.Generic;
using System.Linq;
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

        public void Create(History item)
        {
            db.History.Add(item);
        }

        public void Delete(int id)
        {
            History hs = db.History.FirstOrDefault(x => x.Id == id);

            if (hs != null)
            {
                db.History.Remove(hs);
            }
        }

        public History Get(int id)
        {
            return db.History.Find(id);
        }

        public IEnumerable<History> GetAll()
        {
            return db.History;
        }

        public void Update(History item)
        {
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
