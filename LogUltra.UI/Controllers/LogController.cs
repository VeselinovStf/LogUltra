using LogUltra.UI.Models;
using LogUltra.UI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LogUltra.UI.Controllers
{
    public class LogController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<LogController> _logger; 

        public LogController(
            IHttpClientFactory clientFactory,
            ILogger<LogController> logger)
        {
            this._clientFactory = clientFactory;
            this._logger = logger;
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
                // Datatables request
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

                // Request Model
                var requestModel = new ListLogsRequestModel()
                {
                    Exception = exception,
                    Level = level,
                    PageSize = pageSize,
                    Skip = skip,
                    Source = source,
                    SortColumn = sortColumn,
                    SortColumnDirection = sortColumnDirection,
                    SearchValue = searchValue,
                };

                // Http Client
                var httpClient = _clientFactory.CreateClient();
                httpClient.BaseAddress = new Uri("http://localhost:5227");

                var todoItemJson = new StringContent(
                    JsonConvert.SerializeObject(requestModel),
                    Encoding.UTF8,
                    "application/json");

                using (var httpResponse = await httpClient.PostAsync("/api/logs", todoItemJson))
                {
                    httpResponse.EnsureSuccessStatusCode();

                    if (httpResponse.IsSuccessStatusCode)
                    {
                        var responseStream = await httpResponse.Content.ReadAsStringAsync();

                        var callResponseModel = JsonConvert.DeserializeObject<GetRequestApiResponseModel>(responseStream);

                        if (callResponseModel.Success)
                        {
                            int recordsTotal = callResponseModel.Data.Count();

                            var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = callResponseModel.Data };

                            this._logger.LogInformation(callResponseModel.Message);

                            return Ok(jsonData);
                        }
                        else
                        {
                            this._logger.LogError(httpResponse.Content.ToString());
                        }


                    }
                    else
                    {
                        this._logger.LogError(httpResponse.Content.ToString());
                    }

                }

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
