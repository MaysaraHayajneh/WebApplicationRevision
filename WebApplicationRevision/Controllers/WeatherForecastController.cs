using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplicationRevision.Contratct;
using WebApplicationRevision.Filters;
using WebApplicationRevision.Filters.AuthorizationFilter;
using WebApplicationRevision.Services;

namespace WebApplicationRevision.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[CustomAuthorize]
    //[Authorize]
    public class WeatherForecastController : ControllerBase
    {
        public WeatherForecastController(IServiceProvider serviceProvider, IWeatherforcastService weatherforcastService)
        {
            this.serviceProvider = serviceProvider;
            this.weatherforcastService = weatherforcastService;
        }
        private static readonly string[] Summaries =
        [
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        ];
        private readonly IServiceProvider serviceProvider;
        private readonly IWeatherforcastService weatherforcastService;

        [HttpGet(Name = "GetWeatherForecast")]
        [LogSensetiveDataActionFilter]
        public IEnumerable<WeatherForecast> GetForecasts()
        {
            //var service = serviceProvider.GetKeyedService<IWeatherforcastService>("key1");
            return weatherforcastService.GetForecasts();
        }
    }
}
