using BW.Assessment.Core.Models;
using BW.Assessment.Core.Services;
using BW.Assessment.Infrastructure.Configuration;
using BW.Assessment.Infrastructure.Models;
using BW.Assessment.Infrastructure.Persistence.Repository;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace BW.Assessment.Authentication.Tests
{
	public class AuthenticationServiceTests
	{
		readonly JwtSettings settings;
		public AuthenticationServiceTests()
		{
			settings = new JwtSettings() { Secret = "YMa%tfgkz-4ursfc8?g%mdv6rhJNWYV@HRsn5m9jD$M5u*+#_r+=ZL?cf3Q5E6fDvT*QA@pQQAFWVzE*9JGA+_Ckza@J5rrpuD-YLePK2M#Ez=xP7VY2bAT2BmT7Z4cJBv3uM59!zuh$hcrfpU+h7YKpxbUKd3VFpn48#!!BSEy6CbM_vycC-A#SyBje7j75rWj8tDqEemGP^HEKX6VGJ$UpfTbLcY@?zBQ3$m%mFSE3p*Zc@$t+kP@z*RaJF-bd"
				, Issuer = "test", Audience = "test", Expiry = 120, RefreshExpiry = 1080 };
		}


		[Fact]
		public async void For_UserAuth_Given_InvalidCredentials_Should_Return_Null()
		{
			var request = new TokenRequestDto { Username = "abcdef", Password = "abcdef" };
			var mockJwtSettings = new Mock<IOptions<JwtSettings>>();
			var mockAuthRepository = new Mock<IUserRepository>();

			ILogger<AuthenticationService> loggerMock = Mock.Of<ILogger<AuthenticationService>>();

			mockJwtSettings.Setup(ap => ap.Value).Returns(settings);
			mockAuthRepository.Setup(ap => ap.Authenticate(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult<UserResponseDto>(null));

			var serviceInTest = new AuthenticationService(mockAuthRepository.Object, mockJwtSettings.Object, loggerMock);
			var result = await serviceInTest.Authenticate(request);

			//Assert
			Assert.Null(result);
		}

		[Fact]
		public async void For_UserAuth_Given_ValidCredentials_Should_Return_Token()
		{
			var request = new TokenRequestDto { Username = "admin@mail.com", Password = "P@ssword01" };
			var response = new UserResponseDto("aaaaabbbbbbbbccc1111111", "admin@mail.com");

			var mockJwtSettings = new Mock<IOptions<JwtSettings>>();
			var mockAuthRepository = new Mock<IUserRepository>();
			ILogger<AuthenticationService> loggerMock = Mock.Of<ILogger<AuthenticationService>>();

			mockJwtSettings.Setup(ap => ap.Value).Returns(settings);
			mockAuthRepository.Setup(ap => ap.Authenticate(request.Username, request.Password)).Returns(Task.FromResult<UserResponseDto>(response));

			var serviceInTest = new AuthenticationService(mockAuthRepository.Object, mockJwtSettings.Object, loggerMock);
			var result = await serviceInTest.Authenticate(request);

			//Assert
			Assert.NotEmpty(result.Token);
		}
	}
}
