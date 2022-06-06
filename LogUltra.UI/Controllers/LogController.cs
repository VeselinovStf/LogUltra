using LogUltra.UI.Models;
using LogUltra.UI.Tables;
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
                var datatableReques = new ListLogsDataTableRequest(Request);

                // Request Model
                var requestModel = new ListLogsRequestModel()
                {
                    Exception = datatableReques.Exception,
                    Level = datatableReques.Level,
                    PageSize = datatableReques.PageSize,
                    Skip = datatableReques.Skip,
                    Source = datatableReques.Source,
                    SortColumn = datatableReques.SortColumn,
                    SortColumnDirection = datatableReques.SortColumnDirection,
                    SearchValue = datatableReques.SearchValue,
                };

                // Http Client
                var httpClient = _clientFactory.CreateClient("loguptraapi");

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

                            var jsonData = new { draw = datatableReques.Draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = callResponseModel.Data };

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
