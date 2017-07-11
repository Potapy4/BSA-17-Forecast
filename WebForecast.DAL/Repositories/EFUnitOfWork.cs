using System;
using WebForecast.DAL.Entities;
using WebForecast.DAL.EntityFramework;
using WebForecast.DAL.Interfaces;

namespace WebForecast.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private ForecastContext db;
        private FavoriteCitiesRepository favoriteCitiesRepository;
        private HistoryRepository historyRepository;
        private bool disposed = false;

        public EFUnitOfWork(string connectionString)
        {
            db = new ForecastContext(connectionString);
        }

        public IRepository<City> FavoriteCities
        {
            get
            {
                if (favoriteCitiesRepository == null)
                {
                    favoriteCitiesRepository = new FavoriteCitiesRepository(db);
                }
                return favoriteCitiesRepository;
            }
        }

        public IRepository<History> History
        {
            get
            {
                if (historyRepository == null)
                {
                    historyRepository = new HistoryRepository(db);
                }
                return historyRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
