using AutoMapper;
using BW.Assessment.Wallet.Api.Utilities;
using BW.Assessment.Core.Models;
using BW.Assessment.Core.Services;
using BW.Assessment.Infrastructure.Models;
using BW.Assessment.Infrastructure.Persistence.Repository;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace BW.Assessment.Wallet.Tests
{
	public class WalletServiceTests
	{
		private static IMapper _mapper;

		public WalletServiceTests()
		{
			if (_mapper == null)
			{
				var mappingConfig = new MapperConfiguration(mc =>
				{
					mc.AddProfile(new AutoMapperProfile());
				});
				IMapper mapper = mappingConfig.CreateMapper();
				_mapper = mapper;
			}
		}

		[Fact]
		public async void For_CreateWalletAsync_Given_ValidWalletdDetails_Should_Return_Success_Response()
		{
			var requestDto = new WalletDetailsDto { Balance = 0, UserId = "absdsdsadsfsffsdfcdef", WalletId = 1 };
			var request = new WalletDetails { Balance = 0, UserId = "absdsdsadsfsffsdfcdef", WalletId = 1 };
			var mockWalletRepository = new Mock<IWalletRepository>();

			mockWalletRepository.Setup(ap => ap.CreateWallet(It.IsAny<WalletDetails>())).Returns(Task.FromResult(true));

			var serviceInTest = new WalletService(mockWalletRepository.Object, _mapper);
			var result = await serviceInTest.CreateWalletAsync(requestDto);

			//Assert
			Assert.True(result);
		}

		[Fact]
		public async void For_GetBalanceAsync_Given_InValidUserDetails_Should_Return_Fail_Response()
		{
			var requestDto = new WalletDetailsDto { Balance = 0, UserId = "absdsdsadsfsffsdfcdef" };
			var request = new WalletDetails { Balance = 0, UserId = "absdsdsadsfsffsdfcdef" };
			var mockWalletRepository = new Mock<IWalletRepository>();
			mockWalletRepository.Setup(ap => ap.GetWalletBalanceForUserAsync(It.IsAny<string>())).Returns(Task.FromResult<WalletDetails>(null));

			var serviceInTest = new WalletService(mockWalletRepository.Object, _mapper);
			var result = await serviceInTest.GetWalletBalanceForUserAsync(It.IsAny<string>());

			//Assert
			Assert.Null(result);
		}

		[Fact]
		public async void For_GetBalanceAsync_Given_ValidUserDetails_Should_Return_Fail_Response()
		{
			var userId = "absdsdsadsfsffsdfcdef";
			var responseDto = new WalletDetailsDto { Balance = 0, UserId = "absdsdsadsfsffsdfcdef" };
			var response = new WalletDetails { Balance = 9000, UserId = "absdsdsadsfsffsdfcdef" };
			var mockWalletRepository = new Mock<IWalletRepository>();

			mockWalletRepository.Setup(ap => ap.GetWalletBalanceForUserAsync(It.IsAny<string>())).Returns(Task.FromResult(response));

			var serviceInTest = new WalletService(mockWalletRepository.Object, _mapper);
			var result = await serviceInTest.GetWalletBalanceForUserAsync(userId);

			//Assert
			Assert.Equal(expected: 9000, actual: result.Balance);
		}

		[Fact]
		public async void For_DepositAsync_Given_ValidUserDetails_Should_Return_Success_Response()
		{
			var userId = "absdsdsadsfsffsdfcdef";
			var depositRequestDto = new DepositRequestDto { Amount = 1000, UserId = "absdsdsadsfsffsdfcdef" };
			var request = new WalletDetails { Balance = 9000, UserId = "absdsdsadsfsffsdfcdef" };
			var response = new WalletDetails { Balance = 9000, UserId = "absdsdsadsfsffsdfcdef" };
			var mockWalletRepository = new Mock<IWalletRepository>();

			mockWalletRepository.Setup(ap => ap.GetWalletBalanceForUserAsync(It.IsAny<string>())).Returns(Task.FromResult(response));

			mockWalletRepository.Setup(ap => ap.UpdateWalletDetailsAsync(It.IsAny<WalletDetails>())).Returns(Task.FromResult(true));

			var serviceInTest = new WalletService(mockWalletRepository.Object, _mapper);
			var result = await serviceInTest.DepositIntoWalletAsync(depositRequestDto);

			//Assert
			Assert.True(result);
		}
	}
}
