using System;
using AutoMapper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Blacklamp.Invoice.Core.Dtos;
using Blacklamp.Invoice.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Blacklamp.Invoice.Authentication.Api.Contract.v1.Request;
using Blacklamp.Invoice.Authentication.Api.Contract.v1.Response;

namespace Blacklamp.Invoice.Authentication.Api.Controllers
{
	[ApiVersion("1")]
	[Route("api/v{version:apiVersion}/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly IUserService _userService;

		public UserController(IUserService authenticationService, IMapper mapper)
		{
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
			_userService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
		}

		[AllowAnonymous]
		[HttpPost("Authenticate")]
		[ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
		[ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
		[ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
		public async Task<ActionResult<TokenResponse>> AuthenticateAsync([FromBody] TokenRequest request)
		{
			var response = await _userService.Authenticate(_mapper.Map<TokenRequestDto>(request));
			if(response != null)
			{
				var tokenResponse = _mapper.Map<TokenResponse>(response);
				return Ok(tokenResponse);
			}
			return NotFound("Invalid username or password");
		}
	}
}
