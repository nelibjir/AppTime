using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using AppTime.IoC;
using System.Reflection;
using AppTime.Entities;
using Microsoft.EntityFrameworkCore;
using AppTime.Configs;
using AppTime.Middlewares;

namespace AppTime
{
	public class Startup
	{
		public IConfiguration Configuration { get; }

		private static readonly log4net.ILog fLog = log4net.LogManager.GetLogger(typeof(Startup));
		private const string _ConfigurationSectionName = "DbConnectionConfiguration:ConnectionString";

		public Startup(ILogger<Startup> logger, IConfiguration configuration)
		{
			Configuration = configuration;
		}
		public void ConfigureServices(IServiceCollection services)
		{
			services
				.AddMvc()
				.SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

			services.AddCors();

			DbEnvironment.DefaultDbConnectionString = Configuration
			 .GetSection(_ConfigurationSectionName)
			 .Value;

			services.AddDbContext<DataContext>(SetupDbContext);

			services.AddAdapters();
			services.AddFactories();
			services.AddHandlers();
			services.AddRepositories();
			services.AddServices();
			services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			app.UseCors(CorsConfig.SetupCors);
			app.UseMiddleware<ExceptionMiddleware>();
			app.UseMiddleware<DbTransactionMiddleware>();

			app.UseHsts();
			app.UseHttpsRedirection();

			using (IServiceScope serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
			{
				DataContext context = serviceScope.ServiceProvider.GetRequiredService<DataContext>();
				context.Database.Migrate();
			}

			loggerFactory.AddLog4Net();
			app.UseMvc();
		}

		private void SetupDbContext(DbContextOptionsBuilder optionsBuilder)
		{
			var logger = LoggerFactory.Create(config =>
			{
				config.AddConsole();
			});
			optionsBuilder.UseLoggerFactory(logger);
			optionsBuilder.UseSqlServer(DbEnvironment.DbConnectionString);
		}
	}
}
