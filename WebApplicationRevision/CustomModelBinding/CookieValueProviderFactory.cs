using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebApplicationRevision.CustomModelBinding
{
	public class CookieValueProviderFactory : IValueProviderFactory
	{
		public Task CreateValueProviderAsync(ValueProviderFactoryContext context)
		{
			IRequestCookieCollection cookies = context.ActionContext.HttpContext.Request.Cookies;

			var provider = new CookieValueProvider(cookies);

			context.ValueProviders.Add(provider);

			return Task.CompletedTask;
		}
	}
}
