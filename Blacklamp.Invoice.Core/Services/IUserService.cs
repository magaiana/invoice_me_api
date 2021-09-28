using System.Threading.Tasks;
using Blacklamp.Invoice.Core.Dtos;
using Blacklamp.Invoice.Infrastructure.Entity;

namespace Blacklamp.Invoice.Core.Services
{
	public interface IUserService
	{
		Task<TokenResponseDto> Authenticate(TokenRequestDto request);
		Task<TokenResponseDto> Signup(UserProfileDto user);
	}
}
