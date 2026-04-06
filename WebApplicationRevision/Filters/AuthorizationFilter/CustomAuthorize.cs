using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApplicationRevision.Filters.AuthorizationFilter
{
    [AttributeUsage(AttributeTargets.All)]
    public class CustomAuthorize : Attribute, IAsyncAuthorizationFilter
    {

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
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
