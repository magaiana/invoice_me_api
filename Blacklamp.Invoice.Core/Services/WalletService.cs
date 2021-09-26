using System.Threading.Tasks;
using AutoMapper;
using Blacklamp.Invoice.Core.Dtos;
using Blacklamp.Invoice.Infrastructure.Entity;
using Blacklamp.Invoice.Infrastructure.Persistence.Repository;

namespace Blacklamp.Invoice.Core.Services
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
			var walletDto = _mapper.Map<WalletDetailsDto, WalletDetails>(wallet);
			var result = await _walletRepository.CreateWallet(walletDto);
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
