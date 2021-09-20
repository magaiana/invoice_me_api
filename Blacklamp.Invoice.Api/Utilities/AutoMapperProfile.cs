using AutoMapper;
using Blacklamp.Invoice.Authentication.Api.Contract.v1.Request;
using Blacklamp.Invoice.Authentication.Api.Contract.v1.Response;
using Blacklamp.Invoice.Core.Models;

namespace Blacklamp.Invoice.Authentication.Api.Utilities
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<TokenRequest, TokenRequestDto>();
			CreateMap<TokenResponseDto, TokenResponse>();
		}
	}
}
