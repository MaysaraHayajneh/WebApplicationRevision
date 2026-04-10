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
		private readonly IOptionsMonitor<WeatherOptions> monitor;
		private readonly IOptions<WeatherOptions> _options;
		private readonly IOptionsSnapshot<WeatherOptions> snapshot;

		public GetForecastByNameController(IConfiguration configuration, IOptions<WeatherOptions> options
			, IOptionsSnapshot<WeatherOptions> snapshot, IOptionsMonitor<WeatherOptions> monitor)
		{
			_configuration = configuration;
			this.monitor = monitor;
			_options = options;
			this.snapshot = snapshot;
		}
		[HttpGet]
		[Route("GetByName")]
		public IActionResult GetWeather(string name)
		{
			var configValue = _configuration["Logging:LogLevel:Default"];
			var ENV = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
			var connection = _configuration.GetConnectionString("DefaultConnection");


			var s = _options.Value.City;

			return Ok(new
			{
				Ioption = _options.Value,
				ISnapshot = snapshot.Value,
				IMonitor = monitor.CurrentValue
			});
		}
	}
}
