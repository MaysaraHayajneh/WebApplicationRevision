namespace WebApplicationRevision.OptionPatternsClasses
{
	public class JWTOptions
	{
		public const string SectionName = "JWT";
		public string Issuer { get; set; }
		public string Audience { get; set; }
		public int Lifetime { get; set; }
		public string SigningKey { get; set; }
	}

}
