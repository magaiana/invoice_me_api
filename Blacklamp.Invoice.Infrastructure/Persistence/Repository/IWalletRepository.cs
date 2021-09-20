using System.Threading.Tasks;
using Blacklamp.Invoice.Infrastructure.Models;

namespace Blacklamp.Invoice.Infrastructure.Persistence.Repository
{
	public interface IWalletRepository
	{
		Task<bool> CreateWallet(WalletDetails wallet);
		Task<bool> UpdateWalletDetailsAsync(WalletDetails wallet);
		Task<WalletDetails> GetWalletBalanceForUserAsync(string userId);
	} 
}
