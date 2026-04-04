using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApplicationRevision.Filters
{
	public class LogActivityFilterAsync : IAsyncActionFilter
	{
		private readonly ILogger<LogActivityFilterAsync> logger;

		public LogActivityFilterAsync(ILogger<LogActivityFilterAsync> logger)
		{
			this.logger = logger;
		}
		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			logger.LogInformation($"Executing Action ASYNC on {context.ActionDescriptor.DisplayName} on contoller {context.Controller}");
			await next.Invoke();
			logger.LogInformation($" Action ASYNC finished {context.ActionDescriptor.DisplayName} on contoller {context.Controller}");

		}
	}
}
