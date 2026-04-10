using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApplicationRevision.OptionPatternsClasses;

namespace WebApplicationRevision.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ConfigurationController : ControllerBase
	{
		private readonly IConfiguration configuration;
		private readonly IOptions<WeatherForecast> options;
		private readonly IOptionsSnapshot<NotificationOptions> optionsSnapshot;
		private readonly IOptionsMonitor<NotificationOptions> optionsMonitor;

		public ConfigurationController(IConfiguration configuration, IOptions<WeatherForecast> options
			, IOptionsSnapshot<NotificationOptions> optionsSnapshot, IOptionsMonitor<NotificationOptions> optionsMonitor)
		{
			this.configuration = configuration;
			this.options = options;
			this.optionsSnapshot = optionsSnapshot;
			this.optionsMonitor = optionsMonitor;
		}
		[HttpGet]
		[Route("")]
		public IActionResult GetConfig()
		{
			var iconfig = configuration["NotificationOptions:Email:ApiKey"];
			var iconfig2 = configuration.GetValue<string>("NotificationOptions:Email:ApiKey");

			var ioption = options.Value.ToString();
			var ioptionsnapshot = optionsSnapshot.Value;
			var IOPTIONMONITOR = optionsMonitor.CurrentValue;

			return Ok(ioption);


		}
	}
}
