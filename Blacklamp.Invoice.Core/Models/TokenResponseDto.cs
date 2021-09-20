using AutoMapper;
using Blacklamp.Invoice.Core.Common;
using Blacklamp.Invoice.Infrastructure.Models;

namespace Blacklamp.Invoice.Core.Models
{
	public class TokenResponseDto : EntityDto, IMapFrom
	{
		public string Username { get; set; }
		public string Email { get; set; }
		public string Token { get; set; }
		public bool IsActive { get; set; }

		public void Mapping(Profile profile)
		{
			profile.CreateMap<TokenResponse, TokenResponseDto>().ReverseMap();
		}
	}
}
