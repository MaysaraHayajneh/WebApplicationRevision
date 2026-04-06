using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;

namespace WebApplicationRevision.Filters.RessourceFilter
{
    // Used for short circuit the request by deciding that it do not need full processing 
    // add custom caching 
    public class CacheFilterFilter : IResourceFilter
    {
        private readonly IMemoryCache _memoryCache;
        private readonly int _durationInSec;

        public CacheFilterFilter(IMemoryCache memoryCache, int durationInSec = 60)
        {
            _memoryCache = memoryCache;
            _durationInSec = durationInSec;
        }
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            var key = GenerateCachKey(context);

            if (_memoryCache.TryGetValue(key, out IActionResult? result))
            {
                if (result is not null) context.Result = result;
            }
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            var key = GenerateCachKey(context);

            if (!_memoryCache.TryGetValue(key, out IActionResult? result) && context.Result != null)
            {
                _memoryCache.Set(key, context.Result, TimeSpan.FromSeconds(_durationInSec));
            }
        }

        private string GenerateCachKey(FilterContext context)
        {
            var request = context.HttpContext.Request;

            return $"{request.Path}-{request.QueryString}";
        }
    }


    // Used for short circuit the request by deciding that it do not need full processing 
    // add custom caching 
    public class CacheFilterFilter2 : Attribute, IAsyncResourceFilter
    {
        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            context.Result = new ContentResult
            {
                Content = "This is cached result"
            };
        }
    }


}
