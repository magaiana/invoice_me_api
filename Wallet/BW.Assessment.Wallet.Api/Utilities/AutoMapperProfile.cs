using AutoMapper;
using BW.Assessment.Wallet.Core.Models;
using BW.Wallet.Authentication.Core.Models;
using BW.Wallet.Wallet.Api.Contract.v1.Request;
using BW.Wallet.Wallet.Api.Contract.v1.Response;

namespace BW.Wallet.Wallet.Api.Utilities
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<DepositRequestDto, DepositRequest>();
			CreateMap<CreateWalletRequest, WalletDetailsDto>();			
			CreateMap<WalletDetailsDto, WalletBalanceResponse>();
		}
	}
}
