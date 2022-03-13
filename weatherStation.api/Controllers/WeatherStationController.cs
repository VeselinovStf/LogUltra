using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace weatherStation.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherStationController : ControllerBase
    {
        private readonly ILogger<WeatherStationController> _logger;

        public WeatherStationController(ILogger<WeatherStationController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            _logger.LogInformation("WeatherStationController : Get Wetherstation : Success");

            return Ok(new
            {
                Name = "Weather Station 1",
                Id = 1
            });
        }
    }
}
