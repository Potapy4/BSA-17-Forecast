using System.Net;
using System.Web.Http;
using WebForecast.BLL.BusinessModels.OpenWeatherMap;
using WebForecast.BLL.Interfaces;

namespace WebForecastMVC.API
{
    public class ForecastController : ApiController
    {
        private IBusinessLogic logic;

        public ForecastController(IBusinessLogic logic)
        {
            this.logic = logic;
        }

        // GET api/forecast?city=Lviv&days=3
        public Weather Get([FromUri]string city, int days = 7)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            return logic.GetForecast(city, days);
        }
    }
}
