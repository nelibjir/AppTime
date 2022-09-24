using Microsoft.Extensions.DependencyInjection;
using AppTime.Handlers.Exceptions;

namespace AppTime.IoC
{
	public static class Handlers
	{
		public static void AddHandlers(this IServiceCollection services)
		{
			services.AddTransient<IExceptionHandler, ApiExceptionHandler>();
		}
	}
}
