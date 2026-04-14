using WebApplicationRevision.Services.Contract;

namespace WebApplicationRevision.Services
{
	public class EmailNotificationService : INotificationService
	{
		public string Send(string message)
		{
			//send logic
			return "from email";
		}
	}
}
