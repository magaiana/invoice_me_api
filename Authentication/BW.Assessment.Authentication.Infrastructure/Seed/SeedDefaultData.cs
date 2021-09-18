using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace BW.Assessment.Authentication.Infrastructure.Seed
{
	public class SeedDefaultData
	{
		public static async Task SeedDataAsync(UserManager<IdentityUser> userManager)
		{
            var adminUserName = "admin@mail.com";
            var defaultPassword = "P@ssword01";
            var adminUser = new IdentityUser
            {
                UserName = adminUserName,
                Email = adminUserName,
                EmailConfirmed = true,
            };

            var ee = await userManager.CreateAsync(adminUser, defaultPassword);
            adminUser = await userManager.FindByEmailAsync(adminUserName);
        }
	}
}
