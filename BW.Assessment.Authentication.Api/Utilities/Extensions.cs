using System.Threading.Tasks;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using BW.Assessment.Infrastructure.Seed;
using BW.Assessment.Core.Services;
using Microsoft.AspNetCore.Mvc;
using BW.Assessment.Infrastructure.Persistence.Repository;
using BW.Assessment.Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BW.Assessment.Authentication.Api.Utilities
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
			// Adding Authentication
			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			// Adding Jwt Bearer
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

			services.AddIdentity<IdentityUser, IdentityRole>()
				.AddDefaultTokenProviders()
				.AddUserManager<UserManager<IdentityUser>>()
				.AddSignInManager<SignInManager<IdentityUser>>()
				.AddEntityFrameworkStores<AssessmentDbContext>();

			services.Configure<IdentityOptions>(
				options =>
				{
					options.SignIn.RequireConfirmedEmail = true;
					options.User.RequireUniqueEmail = true;
					options.User.AllowedUserNameCharacters =
						"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
					options.Password.RequireDigit = true;
					options.Password.RequireLowercase = true;
					options.Password.RequireNonAlphanumeric = true;
					options.Password.RequireUppercase = true;
					options.Password.RequiredLength = 6;
					options.Password.RequiredUniqueChars = 1;
				});

			services.AddScoped<IAuthenticationService, AuthenticationService>();
			services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
		}

		public static void AddSwaggerDoc(this IServiceCollection services)
		{
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Betway Assessment Authentication Api", Version = "v1" });
			});
		}

		public static async Task SeedIdentityDataAsync(this IApplicationBuilder builder)
		{
			using var serviceScope =
				builder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
			var services = serviceScope.ServiceProvider;
			var dbContext = services.GetRequiredService<AssessmentDbContext>();
			var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

			dbContext.Database.EnsureCreated();

			await SeedDefaultData.SeedDataAsync(userManager);
		}
	}
}
