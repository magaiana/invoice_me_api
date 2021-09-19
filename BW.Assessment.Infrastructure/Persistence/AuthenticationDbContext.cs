using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using BW.Assessment.Infrastructure.Models;

namespace BW.Assessment.Infrastructure.Persistence
{
	public class AssessmentDbContext : IdentityDbContext<IdentityUser>
	{
		public AssessmentDbContext(DbContextOptions<AssessmentDbContext> options) : base(options)
		{

		}

		public DbSet<WalletDetails> WalletDetails { get; set; }
	}
}
