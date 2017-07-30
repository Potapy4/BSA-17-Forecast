using Ninject.Modules;
using WebForecast.BLL.BusinessModels.Services;
using WebForecast.BLL.Interfaces;
using WebForecast.DAL.Interfaces;
using WebForecast.DAL.Repositories;

namespace WebForecast.BLL.Infrastructure
{
    public class ServiceModule : NinjectModule
    {
        private string connectionString;

        public ServiceModule(string connection)
        {
            connectionString = connection;
        }

        public override void Load()
        {
            Bind<IUnitOfWork>().To<EFUnitOfWork>().WithConstructorArgument(connectionString);
            Bind<IForecastProvider>().To<OpenWeatherMapProvider>().WithConstructorArgument(Properties.Settings.Default.apiKey);
        }
    }
}
