using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Blacklamp.Invoice.Authentication.Api.Utilities;
using Blacklamp.Invoice.Infrastructure.Configuration;

namespace Blacklamp.Invoice.Authentication.Api
{
	public class Startup
	{
		public IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddAutoMapper(typeof(Startup));
			services.AddControllers();
			services.RegisterDependencies(Configuration);
			services.ConfigureApiVersioning();
			services.ConfigureJwt(Configuration);
			services.AddSwaggerDoc();
			services.Configure<JwtSettings>(Configuration.GetSection(nameof(JwtSettings)));
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Blacklamp.Invoice.Authentication.Api v1"));

				app.MigrationInitialisation();
				app.SeedIdentityDataAsync().Wait();
			}

			app.UseHttpsRedirection();

			app.UseAuthentication();
			app.UseRouting();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
