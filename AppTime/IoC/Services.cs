using Microsoft.Extensions.DependencyInjection;
using AppTime.Services;
using AppTime.Services.FileServices;

namespace AppTime.IoC
{
	public static class Services
	{
		public static void AddServices(this IServiceCollection services)
		{
			services.AddTransient<IJsonFormatterService, JsonFormatterService>();
			services.AddTransient<IFileService, FileService>();
		}
	}
}
