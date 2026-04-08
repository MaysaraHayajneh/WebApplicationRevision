using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApplicationRevision.Filters.ActionFilters
{
	public class LogActivityFilterAsync : IAsyncActionFilter
	{
		private readonly ILogger<LogActivityFilterAsync> _logger;

		public LogActivityFilterAsync(ILogger<LogActivityFilterAsync> logger)
		{
			_logger = logger;
		}


		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			_logger.LogInformation("Executing Action {ActionName} with Arguments {@Arguments}",
				context.ActionDescriptor.DisplayName, context.ActionArguments);

			var executedContex = await next.Invoke();

			if (executedContex.Exception is null)
			{
				_logger.LogInformation("Action {ActionName} with Arguments {@Arguments} Executed successfuly",
				context.ActionDescriptor.DisplayName, context.ActionArguments);
			}
			else
			{
				_logger.LogInformation("Action  {ActionName} with Arguments {@Arguments} Executed Wrongly",
			context.ActionDescriptor.DisplayName, context.ActionArguments);
			}

		}
	}
}
