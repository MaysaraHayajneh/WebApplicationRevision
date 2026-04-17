using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace WebApplicationRevision.OptionPatternsClasses.Validators
{
    public class WeatherOptionsValidator : IValidateOptions<WeatherOptions>
    {
        public ValidateOptionsResult Validate(string? name, WeatherOptions options)
        {
            
            var fauilers = new List<string>();

            if (string.IsNullOrWhiteSpace(options.City))
            {
                fauilers.Add($"{nameof(options.City)} is required");
            }

            if (options.Teampreature is < 0 or > 100)
            {
                fauilers.Add($"Temperature must be between 0 and 100. Got: {options.Teampreature}.");
            }

            if (!string.IsNullOrWhiteSpace(options.Summury) && options.Summury.Length > 200)
            {
                fauilers.Add("Summary must be 200 characters or fewer.");
            }

            return fauilers.Count == 0 ? ValidateOptionsResult.Success : ValidateOptionsResult.Fail(fauilers);
        }
    }
}
