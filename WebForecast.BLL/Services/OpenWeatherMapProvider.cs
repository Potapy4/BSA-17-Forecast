using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;
using WebForecast.BLL.BusinessModels.OpenWeatherMap;
using WebForecast.BLL.Interfaces;

namespace WebForecast.BLL.BusinessModels.Services
{
    class OpenWeatherMapProvider : IForecastProvider
    {
        private readonly string apiKey;

        public OpenWeatherMapProvider(string apiKey)
        {
            this.apiKey = apiKey;
        }

        public async Task<Weather> GetForecast(string city, int? days)
        {
            string apiUrl = $"http://api.openweathermap.org/data/2.5/forecast/daily?q={city}&cnt={days}&units=metric&APPID={apiKey}";
            string response = null;
            Weather wr;

            try
            {
                using (WebClient wc = new WebClient())
                {
                    response = await wc.DownloadStringTaskAsync(apiUrl);
                }

                wr = JsonConvert.DeserializeObject<Weather>(response);
            }
            catch (Exception)
            {
                return null;
            }

            return wr;
        }
    }
}
