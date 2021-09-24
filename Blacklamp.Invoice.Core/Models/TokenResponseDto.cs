using AutoMapper;
using Blacklamp.Invoice.Core.Common;
using Blacklamp.Invoice.Infrastructure.Models;

namespace Blacklamp.Invoice.Core.Models
{
	public record TokenResponseDto(string Username,
		string Email,
		string Token,
		bool IsActive
	);
}
