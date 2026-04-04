using System.Reflection;
using WebApplicationRevision.MarkerInterfaces;

namespace WebApplicationRevision
{
	public static class AssemblyRegistartion
	{
		public static IServiceCollection AssemblyRegistartionMethdo(this IServiceCollection serviceCollections)
		{
			var assembly = Assembly.GetExecutingAssembly();
			var types = assembly.GetTypes()
				.Where(t => t.IsClass && !t.IsAbstract && IsEligableRegisteration(t));

			foreach (var type in types)
			{
				var serviceType = type.GetInterfaces()
					.Where(i => i != typeof(IScoped) && i != typeof(ITransient) && i != typeof(ISingleTon))
					.FirstOrDefault();

				if (serviceType is null) continue;

				if (typeof(IScoped).IsAssignableFrom(type))
				{
					serviceCollections.AddScoped(serviceType, type);
				}
				else if (typeof(ITransient).IsAssignableFrom(type))
				{
					serviceCollections.AddTransient(serviceType, type);
				}
				else if (typeof(ISingleTon).IsAssignableFrom(type))
				{
					serviceCollections.AddSingleton(serviceType, type);
				}
			}
			return serviceCollections;
		}


		private static bool IsEligableRegisteration(Type type)
		{
			return typeof(IScoped).IsAssignableFrom(type) || typeof(ITransient).IsAssignableFrom(type)
				|| typeof(ISingleTon).IsAssignableFrom(type);
		}
	}
}
