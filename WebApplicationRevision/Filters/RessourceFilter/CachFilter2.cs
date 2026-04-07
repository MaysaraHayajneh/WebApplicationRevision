// Used for short circuit the request by deciding that it do not need full processing 
// add custom caching 
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;

public class CacheFilterFilter2 : Attribute, IAsyncResourceFilter
{
    private readonly IMemoryCache _memoryCache;
    private readonly int _durationInSec;
    public CacheFilterFilter2(IMemoryCache memoryCache, int durationInSec = 60)
    {
        _memoryCache = memoryCache;
        _durationInSec = durationInSec;
    }

    public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
    {

        var key = GenerateCachKey(context);

        if (_memoryCache.TryGetValue(key, out IActionResult? result1))
        {
            if (result1 is not null) context.Result = result1;
            return;
        }
        var executedContext = await next.Invoke();


        if (!_memoryCache.TryGetValue(key, out IActionResult? result2))
        {
            if (executedContext.Result is ObjectResult { Value: not null } objectResult)
            {
                _memoryCache.Set(key, objectResult, TimeSpan.FromSeconds(_durationInSec));
            }
        }
    }



    private string GenerateCachKey(FilterContext context)
    {
        var request = context.HttpContext.Request;

        return $"{request.Path}-{request.QueryString}";
    }

}
