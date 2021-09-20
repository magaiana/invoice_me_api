using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BW.Assessment.Infrastructure.Configuration;
using BW.Assessment.Shop.Api.Utilities;

namespace BW.Assessment.Shop.Api
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }
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
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BW Assessment Shop Api v1"));
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
