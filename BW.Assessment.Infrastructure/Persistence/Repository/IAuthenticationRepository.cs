using BW.Assessment.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace BW.Assessment.Infrastructure.Persistence.Repository
{
	public interface IAuthenticationRepository
	{
		Task<UserResponseDto> Authenticate(string username, string password);
	} 
}
