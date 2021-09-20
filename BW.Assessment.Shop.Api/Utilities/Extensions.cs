using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BW.Assessment.Core.Services;
using BW.Assessment.Infrastructure.Persistence.Repository;
using BW.Assessment.Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BW.Assessment.Shop.Api.Utilities
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

		public static void ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(options =>
			{
				options.SaveToken = true;
				options.RequireHttpsMetadata = false;
				options.TokenValidationParameters = new TokenValidationParameters()
				{
					ValidateIssuer = false,
					ValidateAudience = false,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:secret"]))
				};
			});
		}

		public static void RegisterDependencies(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<AssessmentDbContext>(option =>
				option.UseSqlServer(configuration.GetConnectionString("DbConnection")));
				//option.UseInMemoryDatabase("InMemoryDbConnection"));

			services.AddScoped<IWalletService, WalletService>();
			services.AddScoped<IWalletRepository, WalletRepository>();
		}

		public static void AddSwaggerDoc(this IServiceCollection services)
		{
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Betway Assessment Shop Api", Version = "v1" });
				c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
				{
					Name = "Authorization",
					Type = SecuritySchemeType.ApiKey,
					Scheme = "Bearer",
					BearerFormat = "JWT",
					In = ParameterLocation.Header,
					Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
				});
				c.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						  new OpenApiSecurityScheme
							{
								Reference = new OpenApiReference
								{
									Type = ReferenceType.SecurityScheme,
									Id = "Bearer"
								}
							},
							System.Array.Empty<string>()
					}
				});
			});
		}
	}
}
