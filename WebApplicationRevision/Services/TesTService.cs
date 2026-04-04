using WebApplicationRevision.Contratct;

namespace WebApplicationRevision.Services
{
	public class TesTService : IWeatherforcastService
	{
		public IEnumerable<WeatherForecast> GetForecasts()
		{
			return new List<WeatherForecast>();
		}
	}
}
