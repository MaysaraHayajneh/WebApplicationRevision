
namespace WebApplicationRevision.Filters.EndPointFilter
{
    public class ValidationEndPointFilter<T> : IEndpointFilter where T : class
    {
        private readonly ILogger<ValidationEndPointFilter<T>> _logger;

        public ValidationEndPointFilter(ILogger<ValidationEndPointFilter<T>> logger)
        {
            _logger = logger;
        }
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {

            var argumets = context.Arguments.OfType<T>().FirstOrDefault();

            if (argumets is null)
            {
                _logger.LogWarning("Request Body For Type {Type} Is Null", typeof(T).Name);

                return Results.BadRequest($"Request Body Of Type {typeof(T).Name} Is Required");
            }

            return await next.Invoke(context);
        }
    }
}
