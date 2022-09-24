using Microsoft.Extensions.DependencyInjection;

namespace AppTime.IoC
{
	public static class Adapters
	{
		public static void AddAdapters(this IServiceCollection services)
		{
			services.AddTransient<IStringUtilAdapter, StringUtilAdapter>();
		}
	}
}
