using WebApplicationRevision.Contratct;
using WebApplicationRevision.MarkerInterfaces;

namespace WebApplicationRevision.Services
{
	public class WeatherForecastService : IWeatherforcastService, ITestService, IScoped
	{
		private static readonly string[] Summaries =
		[
			"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
		];
		private readonly ILogger<WeatherForecastService> logger;

		public WeatherForecastService(ILogger<WeatherForecastService> logger)
		{
			this.logger = logger;
		}
		public IEnumerable<WeatherForecast> GetForecasts()
		{
			logger.LogError("Get FORCEAST");
			return Enumerable.Range(1, 5).Select(index => new WeatherForecast
			{
				Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
				TemperatureC = Random.Shared.Next(-20, 55),
				Summary = Summaries[Random.Shared.Next(Summaries.Length)]
			})
			.ToArray();

		}
	}
}
