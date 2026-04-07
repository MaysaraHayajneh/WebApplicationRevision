using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApplicationRevision.Filters.ResultFiltet
{
	public class ResponseWrappingResultFilter : IAsyncResultFilter
	{
		public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
		{
			var objectResult = context.Result as ObjectResult;

			if (objectResult is not null && objectResult.StatusCode is null or (>= 200 and < 300))
			{
				objectResult.Value = new
				{
					Success = true,
					data = objectResult.Value,
					TimeSpan = DateTime.UtcNow,
					statusCode = objectResult.StatusCode,
				};
			}
			else if (!(objectResult is null || objectResult.StatusCode is null and not >= 400))
			{
				objectResult?.Value = new
				{
					Success = false,
					data = objectResult.Value,
					TimeSpan = DateTime.UtcNow,
					statusCode = objectResult.StatusCode,
				};
			}

			await next.Invoke();
		}
	}
}
