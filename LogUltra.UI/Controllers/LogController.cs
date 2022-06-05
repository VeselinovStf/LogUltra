using AutoMapper;
using LogUltra.Core.Abstraction;
using LogUltra.Log.Service.Models;
using LogUltra.UI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace LogUltra.UI.Controllers
{
    public class LogController : Controller
    {
        private readonly ILogService<GetLogsResponseModel> _logService;
        private readonly ILogger<LogController> _logger;
        private readonly IMapper _mapper;

        public LogController(
            ILogService<GetLogsResponseModel> logService,
            ILogger<LogController> logger,
            IMapper mapper)
        {
            this._logService = logService;
            this._logger = logger;
            this._mapper = mapper;
        }
        // GET: LogController
        public ActionResult Index()
        {
            return View(new List<LogViewModel>());
        }

        [HttpPost]
        public async Task<IActionResult> List()
        {
            try
            {
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault().ToLower();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;


                var level = Request.Form["level"].FirstOrDefault();
                var source = Request.Form["source"].FirstOrDefault();
                var exception = Request.Form["exception"].FirstOrDefault();

                var serviceResponse = await this._logService
                    .GetAsync(sortColumn, sortColumnDirection, searchValue, level, source, exception, pageSize, skip);

                if (serviceResponse.Success)
                {
                    int recordsTotal = serviceResponse.Logs.Count();

                    var mappedLogs = this._mapper.Map<List<LogViewModel>>(serviceResponse.Logs);

                    var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = mappedLogs };

                    this._logger.LogInformation(serviceResponse.Message);

                    return Ok(jsonData);
                }

                this._logger.LogError(serviceResponse.Message);

                return RedirectToAction("Error", "Home");
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message);

                return RedirectToAction("Error", "Home");
            }
        }
    }
}
