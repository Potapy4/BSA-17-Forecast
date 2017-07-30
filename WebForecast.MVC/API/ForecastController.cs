using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
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
        public async Task<Weather> Get([FromUri]string city, int days = 7)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "City name can't be empty!"));
            }

            return await logic.GetForecast(city, days);
        }
    }
}
