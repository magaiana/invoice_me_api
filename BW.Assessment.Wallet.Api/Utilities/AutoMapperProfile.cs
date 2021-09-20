using AutoMapper;
using BW.Assessment.Core.Models;
using BW.Assessment.Infrastructure.Models;
using BW.Wallet.Wallet.Api.Contract.v1.Request;
using BW.Wallet.Wallet.Api.Contract.v1.Response;

namespace BW.Assessment.Wallet.Api.Utilities
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<DepositRequestDto, DepositRequest>();			
			CreateMap<WalletDetailsDto, WalletDetails>().ReverseMap();
			CreateMap<WalletDetailsDto, WalletBalanceResponse>();
		}
	}
}
