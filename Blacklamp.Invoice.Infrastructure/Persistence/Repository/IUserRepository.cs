using Blacklamp.Invoice.Infrastructure.Entity;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Blacklamp.Invoice.Infrastructure.Persistence.Repository
{
	public interface IUserRepository
	{
		Task<UserResponseDto> Authenticate(string username, string password);
		Task<bool> CreateUser(UserProfile userProfile);
		Task<bool> ConfirmEmail(UserProfile userProfile);
		Task<bool> ConfirmCell(UserProfile userProfile);
		Task<UserProfile> GetUserByEmail(string email);
	} 
}
