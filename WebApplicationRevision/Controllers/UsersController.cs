using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplicationRevision.Models;
using WebApplicationRevision.OptionPatternsClasses;

namespace WebApplicationRevision.Controllers
{
	[ApiController]
	[Route("api/[controller]")]

	public class UsersController : ControllerBase
	{
		private readonly JWTOptions snapShot;

		public UsersController(IOptionsSnapshot<JWTOptions> options)
		{
			this.snapShot = options.Value;
		}
		[HttpPost]
		[Route("Auth")]
		public IActionResult AuthenticateUser(AuthenticationRequest request)
		{
			JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

			var secuirtityToken = handler.CreateToken(new SecurityTokenDescriptor()
			{
				Issuer = snapShot.Issuer,
				Audience = snapShot.Audience,
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(snapShot.SigningKey)),
				 SecurityAlgorithms.HmacSha256
				),
				Subject = new ClaimsIdentity(new Claim[]
				{
					new Claim(ClaimTypes.Name,request.UserName),
					new Claim(ClaimTypes.NameIdentifier,request.UserName),
					new Claim(ClaimTypes.Email,$"fasfsa{request.UserName}"),
				})
			});

			var token = handler.WriteToken(secuirtityToken);
			return Ok(token);
		}
	}
}
