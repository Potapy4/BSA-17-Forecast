using System;
using WebForecastMVC.DataBase.Repositories;

namespace WebForecastMVC.DataBase
{
    public class UnitOfWork : IDisposable
    {
        private DBModel db;
        private HistoryRepository history;
        private FavoriteCitiesRepository favcities;
        private bool disposed = false;

        public UnitOfWork()
        {
            db = new DBModel();
            history = new HistoryRepository(db);
            favcities = new FavoriteCitiesRepository(db);
        }

        public HistoryRepository History
        {
            get { return history; }
        }

        public FavoriteCitiesRepository FavoriteCities
        {
            get { return favcities; }
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