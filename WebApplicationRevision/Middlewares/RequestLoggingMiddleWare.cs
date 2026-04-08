
using WebApplicationRevision.MarkerInterfaces;

namespace WebApplicationRevision.Middlewares
{
    public class RequestLoggingMiddleWare(ILogger<RequestLoggingMiddleWare> logger) : ISingleTon, IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var startTime = DateTime.UtcNow;

            logger.LogInformation("Incoming Request {Method} {path}", context.Request.Method, context.Request.Path);

            await next.Invoke(context);

            var elapsedTime = DateTime.UtcNow - startTime;

            logger.LogInformation("Completed request {Method} {Path} with status code {Code} iin {Elapsed}ms ",
                context.Request.Method, context.Request.Path, context.Response.StatusCode, elapsedTime.TotalMicroseconds);
        }
    }
}
