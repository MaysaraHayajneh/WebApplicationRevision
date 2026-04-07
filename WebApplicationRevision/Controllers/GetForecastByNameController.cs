using Microsoft.AspNetCore.Mvc;
using WebApplicationRevision.Filters.ActionFilters;

namespace WebApplicationRevision.Controllers
{
	[Route("api/[controller]")]
	//[ApiController]
	[ModelsStateValidationFilter]
	public class GetForecastByNameController : ControllerBase
	{
		[HttpGet]
		[Route("GetByName")]
		public IActionResult GetWeather(string name)
		{
			return Ok(name);
		}
	}
}
