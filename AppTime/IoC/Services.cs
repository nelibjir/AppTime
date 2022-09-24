using Microsoft.Extensions.DependencyInjection;
using AppTime.Services;

namespace AppTime.IoC
{
	public static class Services
	{
		public static void AddServices(this IServiceCollection services)
		{
			services.AddTransient<IFormatService, FormatterService>();
		}
	}
}
