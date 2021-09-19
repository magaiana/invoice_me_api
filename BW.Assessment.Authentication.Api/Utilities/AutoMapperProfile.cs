using AutoMapper;
using BW.Assessment.Authentication.Api.Contract.v1.Request;
using BW.Assessment.Authentication.Api.Contract.v1.Response;
using BW.Assessment.Core.Models;

namespace BW.Assessment.Authentication.Api.Utilities
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
