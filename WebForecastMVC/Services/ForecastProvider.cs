using System.Net;
using WebForecastMVC.Models.Weather;
using Newtonsoft.Json;
using System;

namespace WebForecastMVC.Services
{
    public class ForecastProvider: IForecastProvider
    {
        private readonly string apiKey = Properties.Settings.Default.apiKey;

        public Weather GetForecast(string city, int days)
        {
            string apiUrl = $"http://api.openweathermap.org/data/2.5/forecast/daily?q={city}&cnt={days}&units=metric&APPID={apiKey}";
            string response = null;
            Weather wr;

            try
            {
                using (WebClient wc = new WebClient())
                {
                    response = wc.DownloadString(apiUrl);
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