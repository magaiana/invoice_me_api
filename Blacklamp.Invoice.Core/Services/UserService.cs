using Blacklamp.Invoice.Core.Dtos;
using Blacklamp.Invoice.Infrastructure.Persistence.Repository;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Blacklamp.Invoice.Core.Common;
using Blacklamp.Invoice.Infrastructure.Entity;
using AutoMapper;

namespace Blacklamp.Invoice.Core.Services
{
	public class UserService : IUserService
	{
		private readonly ITokenHelper _tokenHelper;
		private readonly IMapper _mapper;
		private readonly ILogger<UserService> _logger;
		private readonly IUserRepository _authenticationRepository;
		public UserService(IUserRepository authenticationRepository, ITokenHelper tokenHelper, ILogger<UserService> logger, IMapper mapper)
		{
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
			_tokenHelper = tokenHelper ?? throw new ArgumentNullException(nameof(tokenHelper));
			_authenticationRepository = authenticationRepository ?? throw new ArgumentNullException(nameof(authenticationRepository));
		}

		public async Task<TokenResponseDto> Authenticate(TokenRequestDto request)
		{
			try
			{
				var user = await _authenticationRepository.Authenticate(request.Username, request.Password);
				if (user != null)
				{
					var token = _tokenHelper.GenerateJwtToken(user);
					return new TokenResponseDto(user.Id, user.UserName, user.Email, token, true, false, false);
				}
				return null;
			}
			catch (Exception ex)
			{
				_logger.LogError("Authentication Error: ", ex);
				return null;
			}
		}

		public async Task<TokenResponseDto> Signup(UserProfileDto user)
		{
			try
			{
				var succeeded = await _authenticationRepository.CreateUser(_mapper.Map<UserProfile>(user));
				if (succeeded)
				{
					var _user = await _authenticationRepository.GetUserByEmail(user.Email);
					if (_user != null) {
						var token = _tokenHelper.GenerateJwtToken(_mapper.Map<UserResponseDto>(_user));
						return new TokenResponseDto(user.Id, user.UserName, user.Email, token, true, false, false);
					}
					return null;
				}
				return null;
			}
			catch (Exception ex)
			{
				_logger.LogError("Sign Up Error: ", ex);
				return null;
			}
		}
	}
}
