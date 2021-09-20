using BW.Assessment.Infrastructure.Configuration;
using BW.Assessment.Core.Models;
using BW.Assessment.Infrastructure.Models;
using BW.Assessment.Infrastructure.Persistence.Repository;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace BW.Assessment.Core.Services
{
	public class UserService : IUserService
	{
		private readonly JwtSettings _token;
		private readonly ILogger<UserService> _logger;
		private readonly IUserRepository _authenticationRepository;
		public UserService(IUserRepository authenticationRepository, IOptions<JwtSettings> options, ILogger<UserService> logger)
		{
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
			_authenticationRepository = authenticationRepository  ?? throw new ArgumentNullException(nameof(authenticationRepository));
			_token = options.Value ?? throw new ArgumentNullException(nameof(options.Value));
		}

		public async Task<TokenResponseDto> Authenticate(TokenRequestDto request)
		{
			try
			{
				var user = await _authenticationRepository.Authenticate(request.Username, request.Password);
				if (user != null)
				{
					var token = GenerateJwtToken(user);
					return new TokenResponseDto(user.Id, user.UserName, token);
				}
				return null;
			}
			catch (Exception ex)
			{
				_logger.LogError("Authentication Error: ", ex);
				return null;
			}
		}

		private string GenerateJwtToken(UserResponseDto user)
		{
			try
			{
				var secret = Encoding.ASCII.GetBytes(_token.Secret);

				var handler = new JwtSecurityTokenHandler();
				var descriptor = new SecurityTokenDescriptor
				{
					Issuer = _token.Issuer,
					Audience = _token.Audience,
					Subject = new ClaimsIdentity(new[]
					{
					new Claim("UserId", user.Id),
					new Claim("Email", user.Email),
					new Claim(ClaimTypes.Name, user.UserName)
				}),
					Expires = DateTime.UtcNow.AddMinutes(_token.Expiry),
					SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secret),
						SecurityAlgorithms.HmacSha256Signature)
				};

				var token = handler.CreateToken(descriptor);
				return handler.WriteToken(token);
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
