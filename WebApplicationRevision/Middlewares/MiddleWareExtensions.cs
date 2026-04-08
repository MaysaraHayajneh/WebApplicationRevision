namespace WebApplicationRevision.Middlewares
{
    public static class MiddleWareExtensions
    {
        public static IApplicationBuilder UseRequestLogging(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseMiddleware<RequestLoggingMiddleWare>();
        }
    }
}
