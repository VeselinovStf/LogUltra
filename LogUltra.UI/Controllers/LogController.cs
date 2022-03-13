using AutoMapper;
using LogUltra.UI.Services;
using LogUltra.UI.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace LogUltra.UI.Controllers
{
    public class LogController : Controller
    {
        private readonly LogService _logService;
        private readonly ILogger<LogController> _logger;
        private readonly IMapper _mapper;

        public LogController(
            LogService logService,
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
        public IActionResult List()
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
                int recordsTotal = 0;

                var level = Request.Form["level"].FirstOrDefault();
                var source = Request.Form["source"].FirstOrDefault();
                var exception = Request.Form["exception"].FirstOrDefault();

                var logs = this._logService.Get().AsQueryable();

                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    logs = logs.OrderBy(sortColumn + " " + sortColumnDirection);
                }

                if (!string.IsNullOrEmpty(searchValue))
                {
                    logs = logs.Where(m => m.Id.ToLower().Contains(searchValue)
                                                || m.Description.ToLower().Contains(searchValue)
                                                || m.Source.ToLower().Contains(searchValue)
                                                || m.Exception.ToLower().Contains(searchValue)
                                                || m.CreatedAt.ToString("dd-MM-yyyy hh:mm:ss").ToLower().Contains(searchValue)
                                                || m.Level.ToLower().Contains(searchValue));
                }

                if (!string.IsNullOrWhiteSpace(level))
                {
                    logs = logs.Where(m => m.Level.ToLower().Contains(level.ToLower()));
                }

                if (!string.IsNullOrWhiteSpace(source))
                {
                    logs = logs.Where(m => m.Source.ToLower().Contains(source.ToLower()));
                }

                if (!string.IsNullOrWhiteSpace(exception))
                {
                    logs = logs.Where(m => m.IsException.ToString().ToLower().Contains(exception.ToLower()));
                }


                recordsTotal = logs.Count();
                var data = logs.Skip(skip).Take(pageSize).ToList();

                var mappedLogs = this._mapper.Map<List<LogViewModel>>(data);

                var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = mappedLogs };

                this._logger.LogInformation("Returning Datatables Model");

                return Ok(jsonData);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message);

                return RedirectToAction("Error", "Home");
            }
        }

        // GET: LogController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LogController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LogController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LogController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LogController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LogController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LogController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
