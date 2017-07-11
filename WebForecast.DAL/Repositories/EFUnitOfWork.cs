using System;
using WebForecast.DAL.Entities;
using WebForecast.DAL.EntityFramework;
using WebForecast.DAL.Interfaces;

namespace WebForecast.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private ForecastContext db;
        private FavoriteCitiesRepository phoneRepository;
        private HistoryRepository orderRepository;
        private bool disposed = false;

        public EFUnitOfWork(string connectionString)
        {
            db = new ForecastContext(connectionString);
        }

        public IRepository<City> FavoriteCities
        {
            get
            {
                if (phoneRepository == null)
                {
                    phoneRepository = new FavoriteCitiesRepository(db);
                }
                return phoneRepository;
            }
        }

        public IRepository<History> History
        {
            get
            {
                if (orderRepository == null)
                {
                    orderRepository = new HistoryRepository(db);
                }
                return orderRepository;
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
