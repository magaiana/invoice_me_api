using AutoMapper;
using BW.Assessment.Core.Models;
using BW.Assessment.Shop.Api.Contract.v1.Request;

namespace BW.Assessment.Shop.Api.Utilities
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<ProductDto, Product>().ReverseMap();
			CreateMap<AddStockRequest, StockDto>();
			CreateMap<Stock, StockDto>().ReverseMap();
		}
	}
}
