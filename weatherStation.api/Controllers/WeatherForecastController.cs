using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace weatherStation.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var rng = new Random();

            var model = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();

            _logger.LogInformation($"{nameof(WeatherForecastController)} : Get Weatherforcasts : Success");
            _logger.LogError($"{nameof(WeatherForecastController)} : Get Weatherforcasts : Error");

            return Ok(new
            {
                Data = model
            }); ;
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogError($"{nameof(WeatherForecastController)} : Get Weatherforcast with id {id} : Error");

                return BadRequest();
            }

            _logger.LogInformation($"{nameof(WeatherForecastController)} : Get Weatherforcast with id {id} : Success");


            var rng = new Random();
            var model = Enumerable.Range(1, 1).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();

            return Ok(new
            {
                Data = model
            });
        }
    }
}
