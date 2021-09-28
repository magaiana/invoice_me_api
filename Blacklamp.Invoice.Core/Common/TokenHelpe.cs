using Blacklamp.Invoice.Core.Services;
using Blacklamp.Invoice.Infrastructure.Configuration;
using Blacklamp.Invoice.Infrastructure.Entity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Blacklamp.Invoice.Core.Common
{
	public class TokenHelper : ITokenHelper
	{
		private readonly JwtSettings _token;
		private readonly ILogger<UserService> _logger;
		public TokenHelper(IOptions<JwtSettings> options, ILogger<UserService> logger)
		{
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
			_token = options.Value ?? throw new ArgumentNullException(nameof(options.Value));
		}

		public string GenerateJwtToken(UserResponseDto user)
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
