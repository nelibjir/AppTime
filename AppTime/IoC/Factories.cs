using Microsoft.Extensions.DependencyInjection;
using AppTime.Factories;

namespace AppTime.IoC
{
	public static class Factories
	{
		public static void AddFactories(this IServiceCollection services)
		{
			services.AddTransient<ApiErrorFactory>();
			services.AddTransient<ExceptionHandlerFactory>();
		}
	}
}
