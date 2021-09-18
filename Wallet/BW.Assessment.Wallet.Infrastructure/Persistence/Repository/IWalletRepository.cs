using System.Threading.Tasks;
using BW.Assessment.Wallet.Core.Models;

namespace BW.Assessment.Wallet.Infrastructure.Persistence.Repository
{
	public interface IWalletRepository
	{
		Task<bool> CreateWallet(WalletDetails wallet);
		Task<bool> UpdateWalletDetailsAsync(WalletDetails wallet);
		Task<WalletDetails> GetWalletBalanceForUserAsync(string userId);
	} 
}
