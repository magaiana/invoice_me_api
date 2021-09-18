using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BW.Assessment.Authentication.Api.Utilities;
using BW.Assessment.Authentication.Infrastructure.Configuration;

namespace BW.Assessment.Authentication.Api
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
			services.RegisterDependencies();
			services.ConfigureApiVersioning();
			services.AddSwaggerDoc();
			services.Configure<JwtSettings>(Configuration.GetSection(nameof(JwtSettings)));
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BW.Assessment.Authentication.Api v1"));

				app.SeedIdentityDataAsync().Wait();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
