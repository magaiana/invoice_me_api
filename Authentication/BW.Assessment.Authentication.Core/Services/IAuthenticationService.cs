using System.Threading.Tasks;
using BW.Assessment.Authentication.Core.Models;

namespace BW.Assessment.Authentication.Core.Services
{
	public interface IAuthenticationService
	{
		Task<TokenResponseDto> Authenticate(TokenRequestDto request);
	}
}
