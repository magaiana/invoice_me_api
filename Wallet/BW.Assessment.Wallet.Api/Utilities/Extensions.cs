using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BW.Assessment.Wallet.Core.Services;
using BW.Assessment.Wallet.Infrastructure.Persistence;
using BW.Assessment.Wallet.Infrastructure.Persistence.Repository;

namespace BW.Wallet.Wallet.Api.Utilities
{
	public static class Extensions
	{
		public static void ConfigureApiVersioning(this IServiceCollection services)
		{
			services.AddApiVersioning(setup =>
			{
				setup.DefaultApiVersion = new ApiVersion(1, 0);
				setup.AssumeDefaultVersionWhenUnspecified = true;
				setup.ReportApiVersions = true;
			});
		}

		public static void RegisterDependencies(this IServiceCollection services)
		{
			services.AddDbContext<WalletDbContext>(option =>
				//option.UseSqlServer(configuration.GetConnectionString("DbConnection"))
				option.UseInMemoryDatabase("InMemoryDbConnection"));

			services.AddScoped<IWalletService, WalletService>();
			services.AddScoped<IWalletRepository, WalletRepository>();
		}

		public static void AddSwaggerDoc(this IServiceCollection services)
		{
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Betway Wallet Authentication Api", Version = "v1" });
			});
		}
	}
}
