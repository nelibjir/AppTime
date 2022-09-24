using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Shipvio.Server.Supervisor.Service.Configs
{
	public class SwaggerConfig
	{
		private const string _DocumentName = "supervisor";

		private const string _DocumentRouteTemplate = "/swagger/{documentName}/swagger.json";
		private const string _DocumentEndpoint = "/swagger/supervisor/swagger.json";

		private const string _ApiVersion = "V1";
		private const string _Title = "Supervisor API";

		public static void SetupSwaggerGen(SwaggerGenOptions options)
		{
			options.SwaggerDoc(_DocumentName, new Info()
			{
				Version = _ApiVersion,
				Title = _Title
			});

			options.AddSecurityDefinition(Security._BasicAuthentication, new BasicAuthScheme());
			options.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>> { { Security._BasicAuthentication, Enumerable.Empty<string>() } });
			options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetEntryAssembly().GetName().Name}.xml"));

			options.DescribeAllParametersInCamelCase();
			options.DescribeStringEnumsInCamelCase();
		}

		public static void SetupSwaggerUI(SwaggerUIOptions options)
		{
			options.SwaggerEndpoint(_DocumentEndpoint, _DocumentName);
			options.DisplayRequestDuration();
		}

		public static void SetupSwagger(SwaggerOptions options)
		{
			options.RouteTemplate = _DocumentRouteTemplate;
		}
	}
}
