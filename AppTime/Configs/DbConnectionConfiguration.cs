using AppTime.Entities;

namespace AppTime.Configs
{
	public class DbConnectionConfiguration : IDbConnectionConfiguration
	{
		public string ConnectionString => DbEnvironment.DbConnectionString;
	}
}
