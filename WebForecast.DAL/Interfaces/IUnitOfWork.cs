using System;
using System.Threading.Tasks;
using WebForecast.DAL.Entities;

namespace WebForecast.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<City> FavoriteCities { get; }
        IRepository<History> History { get; }
        Task SaveAsync();
    }
}
