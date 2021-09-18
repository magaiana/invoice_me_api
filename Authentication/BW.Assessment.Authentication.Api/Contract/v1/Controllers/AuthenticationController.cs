using AutoMapper;
using BW.Assessment.Authentication.Api.Contract.v1.Request;
using BW.Assessment.Authentication.Api.Contract.v1.Response;
using BW.Assessment.Authentication.Core.Models;
using BW.Assessment.Authentication.Core.Services;
using BW.Assessment.Authentication.Infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BW.Assessment.Authentication.Api.Controllers
{
	[ApiVersion("1")]
	[Route("api/v{version:apiVersion}/[controller]")]
	[ApiController]
	public class AuthenticationController : ControllerBase
	{
		private readonly IAuthenticationService _authenticationService;
		private readonly IMapper _mapper;

		public AuthenticationController(IAuthenticationService authenticationService, IMapper mapper)
		{
			_mapper = mapper;
			_authenticationService = authenticationService;
		}

		[AllowAnonymous]
		[HttpPost("Authenticate")]
		[ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
		[ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
		[ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
		public async Task<ActionResult<TokenResponse>> AuthenticateAsync([FromBody] TokenRequest request)
		{
			var response = await _authenticationService.Authenticate(_mapper.Map<TokenRequestDto>(request));
			if(response == null)
			{
				return NotFound("Invalid username or password");
			}
			return Ok(response);
		}
	}
}
