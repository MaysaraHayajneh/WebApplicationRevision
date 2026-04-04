namespace WebApplicationRevision.Contratct
{
	public interface IWeatherforcastService
	{
		IEnumerable<WeatherForecast> GetForecasts();

	}
}
