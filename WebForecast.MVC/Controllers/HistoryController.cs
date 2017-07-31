using AutoMapper;
using PagedList;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebForecast.BLL.DTO;
using WebForecast.BLL.Interfaces;
using WebForecastMVC.Models;

namespace WebForecastMVC.Controllers
{
    public class HistoryController : Controller
    {
        private IBusinessLogic logic;

        public HistoryController(IBusinessLogic logic)
        {
            this.logic = logic;
        }

        // GET: History
        public async Task<ActionResult> Index(int? page)
        {
            IEnumerable<HistoryDTO> historyDtos = await logic.GetAllHistoryAsync();
            Mapper.Initialize(cfg => cfg.CreateMap<HistoryDTO, HistoryViewModel>());
            var history = Mapper.Map<IEnumerable<HistoryDTO>, List<HistoryViewModel>>(historyDtos);

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(history.ToPagedList(pageNumber, pageSize));
        }
    }
}