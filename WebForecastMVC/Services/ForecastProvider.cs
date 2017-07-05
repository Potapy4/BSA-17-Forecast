using System.Net;
using WebForecastMVC.Models.Weather;
using Newtonsoft.Json;
using System;

namespace WebForecastMVC.Services
{
    public static class ForecastProvider
    {
        private static readonly string apiKey = Properties.Settings.Default.apiKey;

        public static Weather GetForecast(string city, int days)
        {
            string apiUrl = $"http://api.openweathermap.org/data/2.5/forecast/daily?q={city}&cnt={days}&units=metric&APPID={apiKey}";
            string response = null;
            Weather wr;

            using (WebClient wc = new WebClient())
            {
                response = wc.DownloadString(apiUrl);
            }

            try
            {
                wr = JsonConvert.DeserializeObject<Weather>(response);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return wr;

        }
    }
}