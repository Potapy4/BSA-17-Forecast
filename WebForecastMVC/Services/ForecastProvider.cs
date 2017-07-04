using System.Net;
using WebForecastMVC.Models.Weather;
using Newtonsoft.Json;

namespace WebForecastMVC.Services
{
    public class ForecastProvider
    {
        private readonly string apiKey;
        private readonly string apiUrl;

        public ForecastProvider(string city)
        {
            apiKey = "4ca0f0f0f7e39d870ec2f3c87e5d7c51";
            apiUrl = $"http://api.openweathermap.org/data/2.5/forecast/daily?q={city}&units=metric&APPID={apiKey}";
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