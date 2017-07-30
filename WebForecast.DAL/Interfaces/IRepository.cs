using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebForecast.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);        
        void Create(T item);
        void Update(T item);
        Task DeleteAsync(int id);
    }
}
