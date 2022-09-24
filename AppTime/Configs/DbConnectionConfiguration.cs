namespace AppTime.Configs
{
	public class DbConnectionConfiguration : IDbConnectionConfiguration
	{
		public string ConnectionString => DbEnvironment.DbConnectionString;
	}
}
