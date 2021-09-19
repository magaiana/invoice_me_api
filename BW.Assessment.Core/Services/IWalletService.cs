using System.Threading.Tasks;
using BW.Assessment.Core.Models;

namespace BW.Assessment.Core.Services
{
	public interface IWalletService
	{
		Task<bool> DepositIntoWalletAsync(DepositRequestDto request);
		Task<bool> CreateWalletAsync(WalletDetailsDto request);		
		Task<WalletDetailsDto> GetWalletBalanceForUserAsync(string userId);
		Task<bool> RequestWithdrawal(WithdrawalRequestDto request);
	}
}
