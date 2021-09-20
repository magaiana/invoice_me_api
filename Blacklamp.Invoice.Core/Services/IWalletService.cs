using System.Threading.Tasks;
using Blacklamp.Invoice.Core.Models;

namespace Blacklamp.Invoice.Core.Services
{
	public interface IWalletService
	{
		Task<bool> DepositIntoWalletAsync(DepositRequestDto request);
		Task<bool> CreateWalletAsync(WalletDetailsDto request);		
		Task<WalletDetailsDto> GetWalletBalanceForUserAsync(string userId);
		Task<bool> RequestWithdrawal(WithdrawalRequestDto request);
	}
}
