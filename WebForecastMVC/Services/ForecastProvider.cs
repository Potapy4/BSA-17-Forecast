using System.Net;
using WebForecastMVC.Models.Weather;
using Newtonsoft.Json;

namespace WebForecastMVC.Services
{
    public class ForecastProvider
    {
        private readonly string apiKey;
        private readonly string apiUrl;

        public ForecastProvider(string city, int days)
        {
            apiKey = Properties.Settings.Default.apiKey;
            apiUrl = $"http://api.openweathermap.org/data/2.5/forecast/daily?q={city}&cnt={days}&units=metric&APPID={apiKey}";
        }

        public Weather GetForecast()
        {
            Weather wr;

            try
            {
                string jsonData = Request();
                wr = JsonConvert.DeserializeObject<Weather>(jsonData);
            }
            catch
            {
                wr = null;
            }

            return wr;

        }

        private string Request()
        {
            string response;
            try
            {
                using (WebClient wc = new WebClient())
                {
                    response = wc.DownloadString(apiUrl);
                }
            }
            catch
            {
                response = null;
            }

            return response;
        }
    }
}