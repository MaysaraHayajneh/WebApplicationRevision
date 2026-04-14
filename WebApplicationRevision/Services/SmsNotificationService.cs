using WebApplicationRevision.Services.Contract;

namespace WebApplicationRevision.Services
{
	public class SmsNotificationService : INotificationService
	{
		public string Send(string message)
		{
			return "from sms";
		}
	}
}
