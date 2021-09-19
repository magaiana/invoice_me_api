using AutoMapper;
using BW.Assessment.Authentication.Api.Contract.v1.Request;
using BW.Assessment.Authentication.Api.Controllers;
using BW.Assessment.Core.Models;
using BW.Assessment.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace BW.Assessment.Authentication.Tests
{
	public class AuthenticationControllerTests
	{
		[Fact]
		public async void For_UserAuth_Given_InvalidCredentials_Should_Return_NotFoundActionResult()
		{
			var request = new TokenRequest { Username = "abcdef", Password = "abcdef" };
			var requestDto = new TokenRequestDto { Username = "abcdef", Password = "abcdef" };
			var mockMapper = new Mock<IMapper>();
			var mockAuthService = new Mock<IAuthenticationService>();

			mockMapper.Setup(x => x.Map<TokenRequestDto>(It.IsAny<TokenRequest>())).Returns((TokenRequest source) => requestDto);
			mockAuthService.Setup(ap => ap.Authenticate(requestDto)).Returns(Task.FromResult<TokenResponseDto>(null));

			var controllerInTest = new AuthenticationController(mockAuthService.Object, mockMapper.Object);
			var result = await controllerInTest.AuthenticateAsync(request);

			//Assert
			Assert.IsAssignableFrom<NotFoundObjectResult>(result.Result);
		}

		[Fact]
		public async void For_UserAuth_Given_ValidCredentials_Should_Return_OkActionResult()
		{
			var request = new TokenRequest { Username = "admin@mail.com", Password = "P@ssword01" };
			var requestDto = new TokenRequestDto { Username = "admin@mail.com", Password = "P@ssword01" };
			var response = new TokenResponseDto("aaaaabbbbbbbbccc1111111", "admin@mail.com", "asdffoeroeoreosfjdfdkjhfs");
						
			var mockMapper = new Mock<IMapper>();
			var mockAuthService = new Mock<IAuthenticationService>();

			mockMapper.Setup(x => x.Map<TokenRequestDto>(It.IsAny<TokenRequest>())).Returns((TokenRequest source) => requestDto);
			mockAuthService.Setup(ap => ap.Authenticate(requestDto)).Returns(Task.FromResult<TokenResponseDto>(response));

			var controllerInTest = new AuthenticationController(mockAuthService.Object, mockMapper.Object);
			var result = await controllerInTest.AuthenticateAsync(request);

			//Assert
			Assert.IsAssignableFrom<OkObjectResult>(result.Result);
		}
	}
}
