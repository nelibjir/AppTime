using Microsoft.AspNetCore.Cors.Infrastructure;

namespace AppTime.Configs
{
	public class CorsConfig
	{
		public static void SetupCors(CorsPolicyBuilder cors)
		{
			cors.AllowAnyHeader();
			cors.AllowAnyMethod();
			cors.AllowAnyOrigin();
		}
	}
}
