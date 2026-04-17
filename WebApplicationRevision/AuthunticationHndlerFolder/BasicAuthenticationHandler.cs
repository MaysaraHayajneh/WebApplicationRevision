using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.Extensions.Options;
using System.Buffers.Text;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace WebApplicationRevision.AuthunticationHndlerFolder
{
	public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
	{
		public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
		{
		}

		protected override Task<AuthenticateResult> HandleAuthenticateAsync()
		{
			if (!Request.Headers.ContainsKey("Authorization"))
				return Task.FromResult(AuthenticateResult.NoResult());

			var authorizationHeader = Request.Headers.Authorization.ToString();

			if (!authorizationHeader.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase))
				return Task.FromResult(AuthenticateResult.Fail("Unknow Schema"));

			if (string.IsNullOrWhiteSpace(authorizationHeader)) return Task.FromResult(AuthenticateResult.NoResult());

			var encodedCredentials = authorizationHeader.Substring("Basic ".Length);

			var decodedAuth = Encoding.UTF8.GetString(Convert.FromBase64String(encodedCredentials));

			var userNamePass = decodedAuth.Split(":");

			var username = userNamePass[0];
			var password = userNamePass[1];

			if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password)) return Task.FromResult(AuthenticateResult.NoResult());

			if (string.Equals("maysara", username) && string.Equals(password, "12345"))
			{
				var principal = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
				{
					new Claim(ClaimTypes.NameIdentifier, username),
					new Claim (ClaimTypes.Name,username)

				}, AuthenticationSchemes.Basic.ToString()));

				var authenticationTicket = new AuthenticationTicket(principal, AuthenticationSchemes.Basic.ToString());
				return Task.FromResult(AuthenticateResult.Success(authenticationTicket));
			}
			return Task.FromResult(AuthenticateResult.Fail("Invalid credentials"));
		}
	}
}
