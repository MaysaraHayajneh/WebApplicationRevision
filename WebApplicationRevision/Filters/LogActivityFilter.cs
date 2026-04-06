using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApplicationRevision.Filters
{
	public class LogActivityFilter : IActionFilter
	{
		private readonly ILogger<LogActivityFilter> logger;

		public LogActivityFilter(ILogger<LogActivityFilter> logger)
		{
			this.logger = logger;
		}
		public void OnActionExecuting(ActionExecutingContext context)
		{
			//context.Result = new NotFoundResult(); // will return teh result and will not continue to teh end point logic // short circuit 
			logger.LogInformation($"Executing Action on {context.ActionDescriptor.DisplayName} on contoller {context.Controller}");
		}

		public void OnActionExecuted(ActionExecutedContext context)
		{
			logger.LogInformation($" Action finished {context.ActionDescriptor.DisplayName} on contoller {context.Controller}");

		}

		// The order of filter pipe line i 

	}
}
