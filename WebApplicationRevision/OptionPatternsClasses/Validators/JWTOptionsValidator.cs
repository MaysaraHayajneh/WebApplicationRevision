using Microsoft.Extensions.Options;

namespace WebApplicationRevision.OptionPatternsClasses.Validators
{
	public class JWTOptionsValidator : IValidateOptions<JWTOptions>
	{
		public ValidateOptionsResult Validate(string? name, JWTOptions options)
		{

			if (string.IsNullOrWhiteSpace(options.Issuer))
				return ValidateOptionsResult.Fail("Issure in required");


			if (string.IsNullOrWhiteSpace(options.Audience))
				return ValidateOptionsResult.Fail("Audience in required");


			if (string.IsNullOrWhiteSpace(options.SigningKey))
				return ValidateOptionsResult.Fail("SigningKey in required");

			return ValidateOptionsResult.Success;
		}
	}
}
