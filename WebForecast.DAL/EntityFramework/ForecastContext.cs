namespace WebForecast.DAL.EntityFramework
{
    using System.Data.Entity;
    using WebForecast.DAL.Entities;

    public class ForecastContext : DbContext
    {
        public ForecastContext(string connection)
            : base(connection)
        {
        }

        public virtual DbSet<City> FavoriteCities { get; set; }
        public virtual DbSet<History> History { get; set; }
    }
}