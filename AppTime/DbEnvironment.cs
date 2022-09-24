using System;

namespace AppTime
{
	public class DbEnvironment
	{
		private const EnvironmentVariableTarget _Machine = EnvironmentVariableTarget.Machine;

		public static string DefaultDbConnectionString { get; set; }

		public const string _DbConnectionString = "APP_TIME_DB_CONNECTION_STRING";

		public static string DbConnectionString
		{
			get
			{
				return Environment.GetEnvironmentVariable(_DbConnectionString, _Machine)
				?? DefaultDbConnectionString;
			}
		}
	}
}
