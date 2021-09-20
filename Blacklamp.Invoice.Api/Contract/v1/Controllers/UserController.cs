using AutoMapper;
using Blacklamp.Invoice.Authentication.Api.Contract.v1.Request;
using Blacklamp.Invoice.Authentication.Api.Contract.v1.Response;
using Blacklamp.Invoice.Core.Models;
using Blacklamp.Invoice.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

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
		[HttpGet("Authenticate")]
		[ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
		[ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
		[ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
		public async Task<ActionResult<TokenResponse>> AuthenticateAsync([FromBody] TokenRequest request)
		{
			var response = await _userService.Authenticate(_mapper.Map<TokenRequestDto>(request));
			if(response == null)
			{
				return NotFound("Invalid username or password");
			}
			return Ok(response);
		}
	}
}
