using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BW.Assessment.Authentication.Infrastructure.Persistence
{
	public class AuthenticationDbContext : IdentityDbContext<IdentityUser>
	{
		public AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options) : base(options)
		{

		}
	}
}
