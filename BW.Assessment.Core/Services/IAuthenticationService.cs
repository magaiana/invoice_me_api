using System.Threading.Tasks;
using BW.Assessment.Core.Models;

namespace BW.Assessment.Core.Services
{
	public interface IAuthenticationService
	{
		Task<TokenResponseDto> Authenticate(TokenRequestDto request);
	}
}
