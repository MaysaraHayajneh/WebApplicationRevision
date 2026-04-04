using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApplicationRevision.Filters
{
	public class LogSensetiveDataActionFilter : ActionFilterAttribute
	{
		public override void OnActionExecuted(ActionExecutedContext context)
		{
			base.OnActionExecuted(context);
		}
	}
}
