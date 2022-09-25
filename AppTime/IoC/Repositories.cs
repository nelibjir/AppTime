using AppTime.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace AppTime.IoC
{
	public static class Repositories
	{
		public static void AddRepositories(this IServiceCollection services)
		{
			services.AddTransient<IFileExtensionRepository, FileExtensionRepository>();
		}
	}
}
