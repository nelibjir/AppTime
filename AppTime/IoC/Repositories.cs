using Microsoft.Extensions.DependencyInjection;

namespace AppTime.IoC
{
	public static class Repositories
	{
		public static void AddRepositories(this IServiceCollection services)
		{
			services.AddTransient<IDatasetRepository, DatasetRepository>();
			services.AddTransient<IUserFriendRepository, UserFriendRepository>();
			services.AddTransient<IUserRepository, UserRepository>();
			services.AddTransient<IUserDatasetRepository, UserDatasetRepository>();
		}
	}
}
