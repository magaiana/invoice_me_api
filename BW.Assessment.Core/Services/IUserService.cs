using System.Threading.Tasks;
using BW.Assessment.Core.Models;

namespace BW.Assessment.Core.Services
{
	public interface IUserService
	{
		Task<TokenResponseDto> Authenticate(TokenRequestDto request);
	}
}
