using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog.Formatting.Compact;
using Serilog;
using System;

namespace Blacklamp.Invoice.Authentication.Api
{
	public class Program
	{
		public static void Main(string[] args)
		{
			Log.Logger = new LoggerConfiguration()
			.MinimumLevel.Information()
			.WriteTo.Console(new RenderedCompactJsonFormatter())
			.WriteTo.File("bw.assessment.auth_api_logs.txt", rollingInterval: RollingInterval.Day)
			.CreateLogger();

			try
			{
				Log.Information("Starting Web Host");
				CreateHostBuilder(args).Build().Run();
			}
			catch (Exception ex)
			{
				Log.Fatal(ex, "Host terminated unexpectedly");
			}
			finally
			{
				Log.CloseAndFlush();
			}
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.UseSerilog()
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				});
	}
}
