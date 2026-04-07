using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApplicationRevision.Filters.ActionFilters
{
	public class LogSensetiveDataActionFilter : ActionFilterAttribute
	{
		public override void OnActionExecuted(ActionExecutedContext context)
		{
			base.OnActionExecuted(context);
		}
	}
}
