using BW.Assessment.Wallet.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BW.Assessment.Wallet.Infrastructure.Persistence.Repository
{
	public class WalletRepository : IWalletRepository
	{
		private readonly WalletDbContext _dbContext;
		public WalletRepository(WalletDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<bool> CreateWallet(WalletDetails wallet)
		{
			_dbContext.WalletDetails.Add(wallet);
			var changes = await _dbContext.SaveChangesAsync();
			return changes > 0;
		}

		public async Task<bool> UpdateWalletDetailsAsync(WalletDetails wallet)
		{
			_dbContext.Entry(wallet).State = EntityState.Modified;
			var changes = await _dbContext.SaveChangesAsync();
			return changes > 0;
		}

		public async Task<WalletDetails> GetWalletBalanceForUserAsync(string userId)
		{
			var wallet = await _dbContext.WalletDetails.FirstOrDefaultAsync(x => x.UserId == userId);
			return wallet;
		}
	}
}
