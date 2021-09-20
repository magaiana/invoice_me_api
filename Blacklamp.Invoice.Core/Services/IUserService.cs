using System.Threading.Tasks;
using Blacklamp.Invoice.Core.Models;

namespace Blacklamp.Invoice.Core.Services
{
	public interface IUserService
	{
		Task<TokenResponseDto> Authenticate(TokenRequestDto request);
	}
}
