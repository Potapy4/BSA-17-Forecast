using System;
using WebForecast.DAL.Entities;

namespace WebForecast.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<City> FavoriteCities { get; }
        IRepository<History> History { get; }
        void Save();
    }
}
