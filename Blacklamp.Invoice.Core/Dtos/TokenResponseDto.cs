using AutoMapper;
using Blacklamp.Invoice.Core.Common;
using Blacklamp.Invoice.Infrastructure.Entity;

namespace Blacklamp.Invoice.Core.Dtos
{
	public record TokenResponseDto(string Username,
		string Email,
		string Token,
		bool IsActive
	);
}
