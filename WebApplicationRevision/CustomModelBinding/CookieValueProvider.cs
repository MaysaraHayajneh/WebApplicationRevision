using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebApplicationRevision.CustomModelBinding
{
	public class CookieValueProvider : IValueProvider
	{
		private readonly IRequestCookieCollection _cookie;

		public CookieValueProvider(IRequestCookieCollection cookie)
		{
			_cookie = cookie;
		}
		public bool ContainsPrefix(string prefix)
		{
			return _cookie.ContainsKey(prefix);
		}

		public ValueProviderResult GetValue(string key)
		{
			if (_cookie.TryGetValue(key, out var value))
			{
				return new ValueProviderResult(value);
			}
			return ValueProviderResult.None;
		}
	}
}
