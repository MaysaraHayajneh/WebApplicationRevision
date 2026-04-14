using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;
using WebApplicationRevision.Constants.enums;
using WebApplicationRevision.OptionPatternsClasses;
using WebApplicationRevision.Services.Contract;

namespace WebApplicationRevision.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class NotificationController : ControllerBase
	{

		private readonly IOptionsSnapshot<NotificationOptions> snapshot;
		private readonly INotificationService smsService;
		private readonly INotificationService emailService;

		public NotificationController(IOptionsSnapshot<NotificationOptions> snapshot,
			[FromKeyedServices(NotificationEnum.SMS)] INotificationService smsService
			, [FromKeyedServices(NotificationEnum.EMAIL)] INotificationService emailService)

		{
			this.snapshot = snapshot;
			this.smsService = smsService;
			this.emailService = emailService;
		}
		[HttpGet]
		public IActionResult Notification()
		{
			var emailOptions = snapshot.Get(NotificationOptions.Email);
			var smsOptions = snapshot.Get(NotificationOptions.SMS);

			return Ok();
		}

		[HttpGet]
		[Route("NotificationMethod")]
		public IActionResult NotificationMethod(string mesg)
		{

			return Ok(smsService.Send(mesg));
		}

	}
}
