using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GlobalizationLocalization.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace GlobalizationLocalization.Controllers.WeatherForecast2
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        // Globalization
        private readonly IStringLocalizer<WeatherForecastController> _localizer;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        // Globalization Injection
        public WeatherForecastController(ILogger<WeatherForecastController> logger, IStringLocalizer<WeatherForecastController> localizer)
        {
            _logger = logger;
            _localizer = localizer;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [Route("GetWeather")]
        [HttpGet]
        public ActionResult<WeatherForecast> GetWeather()
        {
            var rng = new Random();
            return Ok(Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray());
        }

        [Route("GetPerson")]
        [HttpGet]
        public ActionResult<Result> GetPerson()
        {
            var rng = new Random();
            var person = new Person
            {
                Name = _localizer["Parameter"],
                Age = 25
            };
            return Ok(new Result {
                IsValid = true,
                Person = person,
                Parameter = _localizer["Parameter"]
            });
        }
    }
}
