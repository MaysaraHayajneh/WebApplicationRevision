using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplicationRevision.Contratct;
using WebApplicationRevision.Filters;
using WebApplicationRevision.Filters.AuthorizationFilter;
using WebApplicationRevision.Filters.TestTypeFilter;
using WebApplicationRevision.Services;

namespace WebApplicationRevision.Controllers
{
    [ApiController]
    [Route("[controller]")]

    //[ServiceFilter(typeof(CustomAuthorize))]  // service filter gives you teh ability ti use teh filter as service and get it by using the DI container soo you
    //can resolve teh dependency in it's constructor easly 

    //[TypeFilter(typeof(CheckLoggedRoleFilter), Arguments = new object[] { "Admin" })]
    /// TypeFilter  gives you teh ability ti use teh filter as service and get it by using the DI container soo you
    //can resolve teh dependency in it's constructor easly  with ability to pass argumnets which makes it more flexible 

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
        //[LogSensetiveDataActionFilter]
        public IActionResult GetForecasts()
        {
            //var service = serviceProvider.GetKeyedService<IWeatherforcastService>("key1");
            return Ok(weatherforcastService.GetForecasts());
        }
    }
}
