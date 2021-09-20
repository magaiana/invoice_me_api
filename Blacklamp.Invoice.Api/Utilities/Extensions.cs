using System.Text;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc;
using Blacklamp.Invoice.Core.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Blacklamp.Invoice.Infrastructure.Seed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Blacklamp.Invoice.Infrastructure.Persistence;
using Blacklamp.Invoice.Infrastructure.Persistence.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Blacklamp.Invoice.Infrastructure.Models;

namespace Blacklamp.Invoice.Authentication.Api.Utilities
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

			services.AddIdentity<UserProfile, IdentityRole>()
				.AddDefaultTokenProviders()
				.AddUserManager<UserManager<UserProfile>>()
				.AddSignInManager<SignInManager<UserProfile>>()
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

			services.AddScoped(typeof(IUserService), typeof(UserService));
			services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
		}

		public static void AddSwaggerDoc(this IServiceCollection services)
		{
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Betway Invoice Authentication Api", Version = "v1" });
			});
		}

		public static async Task SeedIdentityDataAsync(this IApplicationBuilder builder)
		{
			using var serviceScope =
				builder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
			var services = serviceScope.ServiceProvider;
			var dbContext = services.GetRequiredService<AssessmentDbContext>();
			var userManager = services.GetRequiredService<UserManager<UserProfile>>();

			dbContext.Database.EnsureCreated();
			await SeedDefaultData.SeedDataAsync(userManager);
		}

		public static void MigrationInitialisation(this IApplicationBuilder app)
		{
			using var serviceScope = app.ApplicationServices.CreateScope();
			serviceScope.ServiceProvider.GetService<AssessmentDbContext>().Database.Migrate();
		}
	}
}
