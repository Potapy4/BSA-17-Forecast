using AutoMapper;
using System.Collections.Generic;
using System.Web.Http;
using WebForecast.BLL.DTO;
using WebForecast.BLL.Interfaces;
using WebForecastMVC.Models;

namespace WebForecastMVC.API
{
    public class HistoryController : ApiController
    {
        private IBusinessLogic logic;

        public HistoryController(IBusinessLogic logic)
        {
            this.logic = logic;
        }

        // GET api/History
        public IEnumerable<HistoryViewModel> Get()
        {
            IEnumerable<HistoryDTO> historyDtos = logic.GetAllHistory();
            Mapper.Initialize(cfg => cfg.CreateMap<HistoryDTO, HistoryViewModel>());
            var history = Mapper.Map<IEnumerable<HistoryDTO>, List<HistoryViewModel>>(historyDtos);

            return history;
        }
    }
}