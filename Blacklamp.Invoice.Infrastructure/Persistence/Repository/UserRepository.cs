using Blacklamp.Invoice.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace Blacklamp.Invoice.Infrastructure.Persistence.Repository
{
	public class UserRepository : IUserRepository
	{
		private readonly UserManager<UserProfile> _userManager;
		public UserRepository(UserManager<UserProfile> userManager)
		{
			_userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
		}

		public async Task<UserResponseDto> Authenticate(string username, string password)
		{
			(bool isValidUser, UserProfile user) result = await IsValidUser(username, password);
			if (result.isValidUser)
			{
				if (result.user != null)
				{
					await _userManager.UpdateAsync(result.user);
					return new UserResponseDto(result.user.Id, result.user.UserName);
				}
			}
			return null;
		}

		private async Task<(bool, UserProfile)> IsValidUser(string username, string password)
		{
			var user = await _userManager.FindByEmailAsync(username);
			if (user == null)
			{
				return (false, null);
			}

			var isValidPassword = await _userManager.CheckPasswordAsync(user, password);
			if (isValidPassword)
			{
				return (true, user);
			}

			return (false, null);
		}
	}
}
