using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApplicationRevision.Filters.ExceptionFilter
{
	public class GlobalExceptionFilter : IExceptionFilter
	{
		private readonly ILogger<GlobalExceptionFilter> _logger;

		public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
		{
			_logger = logger;
		}
		public void OnException(ExceptionContext context)
		{

			_logger.LogError(context.Exception, "Unhandled Exception in {ActionName}", context.ActionDescriptor);

			ProblemDetails problem = new ProblemDetails()
			{
				Status = StatusCodes.Status500InternalServerError,
				Detail = context.Exception.Message,
				Title = "Un Handled Exception",
				Instance = context.HttpContext.Request.Path
			};


			context.Result = new ObjectResult(problem)
			{
				StatusCode = StatusCodes.Status500InternalServerError
			};

			context.ExceptionHandled = true;

		}
	}
}
