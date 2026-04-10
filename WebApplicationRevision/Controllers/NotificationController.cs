using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApplicationRevision.OptionPatternsClasses;

namespace WebApplicationRevision.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class NotificationController : ControllerBase
	{
		private readonly IOptionsSnapshot<NotificationOptions> snapshot;

		public NotificationController(IOptionsSnapshot<NotificationOptions> snapshot)
		{
			this.snapshot = snapshot;
		}
		[HttpGet]
		public IActionResult Notification()
		{
			var emailOptions = snapshot.Get(NotificationOptions.Email);
			var smsOptions = snapshot.Get(NotificationOptions.SMS);

			return Ok();
		}
	}
}
