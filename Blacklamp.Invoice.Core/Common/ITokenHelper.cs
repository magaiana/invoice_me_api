using Blacklamp.Invoice.Infrastructure.Entity;

namespace Blacklamp.Invoice.Core.Common
{
	public interface ITokenHelper
	{
		string GenerateJwtToken(UserResponseDto user);
	}
}
