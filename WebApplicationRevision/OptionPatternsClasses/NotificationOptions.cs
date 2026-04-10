namespace WebApplicationRevision.OptionPatternsClasses
{
	public class NotificationOptions
	{
		public const string Email = "Email";

		public const string SMS = "SMS";
		public string ApiKey { get; set; }
		public string Sender { get; set; }

	}
}
