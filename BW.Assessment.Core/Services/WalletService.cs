using System.Threading.Tasks;
using AutoMapper;
using BW.Assessment.Core.Models;
using BW.Assessment.Infrastructure.Models;
using BW.Assessment.Infrastructure.Persistence.Repository;

namespace BW.Assessment.Core.Services
{
	public class WalletService : IWalletService
	{
		private readonly IMapper _mapper;
		private readonly IWalletRepository _walletRepository;
		public WalletService(IWalletRepository walletRepository, IMapper mapper)
		{
			_mapper = mapper;
			_walletRepository = walletRepository;
		}

		public async Task<bool> CreateWalletAsync(WalletDetailsDto wallet)
		{
			var result = await _walletRepository.CreateWallet(_mapper.Map<WalletDetailsDto, WalletDetails>(wallet));
			return result;
		}

		public async Task<bool> DepositIntoWalletAsync(DepositRequestDto request)
		{
			var wallet = await _walletRepository.GetWalletBalanceForUserAsync(request.UserId);
			if(wallet != null)
			{
				wallet.Balance += request.Amount;
				var response = await _walletRepository.UpdateWalletDetailsAsync(wallet);
				return response;
			}
			return false;	
		}

		public async Task<WalletDetailsDto> GetWalletBalanceForUserAsync(string userId)
		{
			var wallet = await _walletRepository.GetWalletBalanceForUserAsync(userId);
			if (wallet != null)
			{
				return _mapper.Map<WalletDetails, WalletDetailsDto>(wallet);
			}
			return null;
		}

		public async Task<bool> RequestWithdrawal(WithdrawalRequestDto request)
		{
			var wallet = await _walletRepository.GetWalletBalanceForUserAsync(request.UserId);
			if (wallet != null)
			{
				wallet.Balance -= request.Amount;
				var response = await _walletRepository.UpdateWalletDetailsAsync(wallet);
				return response;
			}
			return false;
		}
	}
}
