using BW.Assessment.Authentication.Infrastructure.Configuration;
using BW.Assessment.Authentication.Core.Models;
using BW.Assessment.Authentication.Infrastructure.Models;
using BW.Assessment.Authentication.Infrastructure.Persistence.Repository;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BW.Assessment.Authentication.Core.Services
{
	public class AuthenticationService : IAuthenticationService
	{
		private readonly JwtSettings _token;
		private readonly IAuthenticationRepository _authenticationRepository;
		public AuthenticationService(IAuthenticationRepository authenticationRepository,IOptions<JwtSettings> options)
		{
			_authenticationRepository = authenticationRepository;
			_token = options.Value;
		}
		
		public async Task<TokenResponseDto> Authenticate(TokenRequestDto request)
		{
			var user = await _authenticationRepository.Authenticate(request.Username, request.Password);
			if(user != null)
			{
				var token = GenerateJwtToken(user);
				return new TokenResponseDto(user.Id, user.UserName, token);
			}
			return null;
		}

		private string GenerateJwtToken(UserResponseDto user)
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
	}
}
