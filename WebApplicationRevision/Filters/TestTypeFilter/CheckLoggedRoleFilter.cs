using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApplicationRevision.Filters.TestTypeFilter
{
    public class CheckLoggedRoleFilter : Attribute, IAsyncResourceFilter
    {
        private readonly string _roleName;
        private readonly ILogger _logger;

        public CheckLoggedRoleFilter(string roleName, ILogger<CheckLoggedRoleFilter> logger)
        {
            _roleName = roleName;
            _logger = logger;
        }
        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            var currentUserRole = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Role")?.Value;

            if (!string.Equals(_roleName, currentUserRole, StringComparison.OrdinalIgnoreCase))
            {
                context.Result = new UnauthorizedObjectResult(new ProblemDetails()
                {
                    Status = StatusCodes.Status401Unauthorized,
                    Title = "Unauthorized , you are not admin",
                });
            }

        }
    }
}
