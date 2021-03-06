﻿using AutoMapper;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebForecast.BLL.BusinessModels.OpenWeatherMap;
using WebForecast.BLL.DTO;
using WebForecast.BLL.Interfaces;
using WebForecastMVC.Models;

namespace WebForecastMVC.Controllers
{
    public class ForecastController : Controller
    {
        private IBusinessLogic logic;

        public ForecastController(IBusinessLogic logic)
        {
            this.logic = logic;
        }

        // GET: Forecast
        public async Task<ActionResult> Index(string city, int? days)
        {
            // No access without params
            if (city == null || days == null)
            {
                return RedirectToAction("Index", "Home");
            }

            Weather wr = await logic.GetForecastAsync(city, days).ConfigureAwait(false);

            if (wr == null)
            {
                return View("Error", model: "The problem with forecast provider. Please try again later.");
            }

            await LogInDb(wr).ConfigureAwait(false);
            return View(wr);
        }

        private async Task LogInDb(Weather wr)
        {
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(wr.List[0].Dt);

            Mapper.Initialize(cfg => cfg.CreateMap<HistoryViewModel, HistoryDTO>());
            var historyDto = Mapper.Map<HistoryViewModel, HistoryDTO>(new HistoryViewModel()
            {
                City = wr.City.Name,
                LogTime = DateTime.Now,
                ForecastDate = dt,
                TempMin = wr.List[0].Temp.Min,
                TempMax = wr.List[0].Temp.Max,
                Wind = wr.List[0].Speed,
                Humidity = wr.List[0].Humidity,
                Summary = wr.List[0].Weather[0].Description
            });

            await logic.LogIntoHistoryAsync(historyDto);
        }
    }
}