using System.Threading.Tasks;
using BW.Assessment.Infrastructure.Models;

namespace BW.Assessment.Infrastructure.Persistence.Repository
{
	public interface IWalletRepository
	{
		Task<bool> CreateWallet(WalletDetails wallet);
		Task<bool> UpdateWalletDetailsAsync(WalletDetails wallet);
		Task<WalletDetails> GetWalletBalanceForUserAsync(string userId);
	} 
}
