using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApplicationRevision.MarkerInterfaces;

namespace WebApplicationRevision.Filters.AuthorizationFilter
{
    [AttributeUsage(AttributeTargets.All)]
    public class CustomAuthorize : Attribute, IAsyncAuthorizationFilter
    {
        private readonly IConfiguration _configuration;

        public CustomAuthorize(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (context.HttpContext.Request.Headers.TryGetValue("Authorization", out var token))
            {
                var expectedKey = _configuration["AuthorizationKey"];

                if (token != expectedKey)
                {
                    context.Result = new UnauthorizedObjectResult(new ProblemDetails()
                    {
                        Status = StatusCodes.Status401Unauthorized,
                        Title = "Unauthorized , INvalid Api key",
                    });
                }
            }
            else
            {
                //context.Result = new UnauthorizedObjectResult(new ProblemDetails()
                //{
                //    Status = StatusCodes.Status401Unauthorized,
                //    Title = "Unauthorized , INvalid Api key",
                //});
            }
        }
    }


    [AttributeUsage(AttributeTargets.All)]
    public class CustomAuthorize2 : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.Request.Headers.TryGetValue("Authorization", out var token))
            {
                if (token != "mysecre") context.Result = new UnauthorizedResult();

            }
            else
            {
                context.Result = new UnauthorizedResult();
            }
        }

    }

}
