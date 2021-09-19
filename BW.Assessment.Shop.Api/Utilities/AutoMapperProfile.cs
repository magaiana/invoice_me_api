using AutoMapper;
using BW.Assessment.Core.Models;
using BW.Wallet.Wallet.Api.Contract.v1.Request;

namespace BW.Wallet.Wallet.Api.Utilities
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<ProductDto, Product>().ReverseMap();
			CreateMap<AddStockRequest, StockDto>();
		}
	}
}
