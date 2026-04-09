using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApplicationRevision.Filters.ActionFilters;
using WebApplicationRevision.OptionPatternsClasses;

namespace WebApplicationRevision.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    [ModelsStateValidationFilter]
    public class GetForecastByNameController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly WeatherOptions _options;

        public GetForecastByNameController(IConfiguration configuration, IOptions<WeatherOptions> options)
        {
            _configuration = configuration;
            _options = options.Value;
        }
        [HttpGet]
        [Route("GetByName")]
        public IActionResult GetWeather(string name)
        {
            var configValue = _configuration["Logging:LogLevel:Default"];
            var ENV = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var connection = _configuration.GetConnectionString("DefaultConnection");
            var s = _options.City;

            return Ok(_options);
        }
    }
}
