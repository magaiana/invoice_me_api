using AutoMapper;
using BW.Assessment.Core.Models;
using BW.Assessment.Core.Services;
using BW.Assessment.Infrastructure.Models;
using BW.Assessment.Infrastructure.Persistence.Repository;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace BW.Assessment.Wallet.Tests
{
	public class ShopServiceTests
	{
		[Fact]
		public async void For_CreateWalletAsync_Given_ValidWalletdDetails_Should_Return_Success_Response()
		{
			var requestDto = new WalletDetailsDto { Balance = 0, UserId = "absdsdsadsfsffsdfcdef", WalletId = 1 };
			var request = new WalletDetails { Balance = 0, UserId = "absdsdsadsfsffsdfcdef", WalletId = 1 };
			var mockWalletRepository = new Mock<IWalletRepository>();
			var mockMapper = new Mock<IMapper>();
			mockMapper.Setup(x => x.Map<WalletDetails>(It.IsAny<WalletDetailsDto>()))
				.Returns((WalletDetailsDto source) => request);

			mockWalletRepository.Setup(ap => ap.CreateWallet(request)).Returns(Task.FromResult(true));

			var serviceInTest = new WalletService(mockWalletRepository.Object, mockMapper.Object);
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
			var mockMapper = new Mock<IMapper>();
			mockMapper.Setup(x => x.Map<WalletDetails>(It.IsAny<WalletDetailsDto>()))
				.Returns((WalletDetailsDto source) => request);

			mockWalletRepository.Setup(ap => ap.GetWalletBalanceForUserAsync(It.IsAny<string>())).Returns(Task.FromResult<WalletDetails>(null));

			var serviceInTest = new WalletService(mockWalletRepository.Object, mockMapper.Object);
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

			var mockMapper = new Mock<IMapper>();
			mockMapper.Setup(x => x.Map<WalletDetailsDto>(response))
				.Returns((WalletDetailsDto source) => responseDto);

			mockWalletRepository.Setup(ap => ap.GetWalletBalanceForUserAsync(userId)).Returns(Task.FromResult<WalletDetails>(response));

			var serviceInTest = new WalletService(mockWalletRepository.Object, mockMapper.Object);
			var result = await serviceInTest.GetWalletBalanceForUserAsync(userId);

			//Assert
			Assert.Equal(expected: 900, actual: result.Balance);
		}

		[Fact]
		public async void For_DepositAsync_Given_InValidUserDetails_Should_Return_Fail_Response()
		{
			var userId = "absdsdsadsfsffsdfcdef";
			var responseDto = new WalletDetailsDto { Balance = 0, UserId = "absdsdsadsfsffsdfcdef" };
			var response = new WalletDetails { Balance = 9000, UserId = "absdsdsadsfsffsdfcdef" };
			var mockWalletRepository = new Mock<IWalletRepository>();

			var mockMapper = new Mock<IMapper>();
			mockMapper.Setup(x => x.Map<WalletDetailsDto>(It.IsAny<WalletDetails>()))
				.Returns((WalletDetails source) => responseDto);

			mockWalletRepository.Setup(ap => ap.GetWalletBalanceForUserAsync(userId)).Returns(Task.FromResult<WalletDetails>(null));

			var serviceInTest = new WalletService(mockWalletRepository.Object, mockMapper.Object);
			var result = await serviceInTest.GetWalletBalanceForUserAsync(userId);

			//Assert
			Assert.Equal(expected: 900, actual: result.Balance);
		}

		[Fact]
		public async void For_DepositAsync_Given_ValidUserDetails_Should_Return_Success_Response()
		{
			var userId = "absdsdsadsfsffsdfcdef";
			var depositRequestDto = new DepositRequestDto { Amount = 1000, UserId = "absdsdsadsfsffsdfcdef" };
			var request = new WalletDetails { Balance = 9000, UserId = "absdsdsadsfsffsdfcdef" };
			var response = new WalletDetails { Balance = 9000, UserId = "absdsdsadsfsffsdfcdef" };
			var mockWalletRepository = new Mock<IWalletRepository>();

			var mockMapper = new Mock<IMapper>();
			mockMapper.Setup(x => x.Map<WalletDetails>(It.IsAny<WalletDetailsDto>()))
				.Returns((WalletDetails source) => response);

			mockWalletRepository.Setup(ap => ap.GetWalletBalanceForUserAsync(userId)).Returns(Task.FromResult(response));

			mockWalletRepository.Setup(ap => ap.UpdateWalletDetailsAsync(request)).Returns(Task.FromResult<bool>(true));

			var serviceInTest = new WalletService(mockWalletRepository.Object, mockMapper.Object);
			var result = await serviceInTest.GetWalletBalanceForUserAsync(userId);

			//Assert
			Assert.Equal(expected: 10000, actual: result.Balance);
		}
	}
}
