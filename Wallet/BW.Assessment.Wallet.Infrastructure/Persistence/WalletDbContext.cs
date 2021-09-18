using Microsoft.EntityFrameworkCore;
using BW.Assessment.Wallet.Core.Models;

namespace BW.Assessment.Wallet.Infrastructure.Persistence
{
	public class WalletDbContext : DbContext
	{
		public WalletDbContext(DbContextOptions<WalletDbContext> options) : base(options)
		{

		}

		public DbSet<WalletDetails> WalletDetails { get; set; }
	}
}
