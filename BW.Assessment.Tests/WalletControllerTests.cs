using AutoMapper;
using BW.Assessment.Core.Models;
using BW.Assessment.Core.Services;
using BW.Assessment.Wallet.Api.Utilities;
using BW.Wallet.Wallet.Api.Contract.v1.Controllers;
using BW.Wallet.Wallet.Api.Contract.v1.Request;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace BW.Assessment.Wallet.Tests
{
	public class WalletControllerTests
	{
		private static IMapper _mapper;

		public WalletControllerTests()
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
		public async void For_CreateWalletAsync_Given_ValidWalletdDetails_Should_Return_OkActionResult()
		{			
			var mockWalletService = new Mock<IWalletService>();
			mockWalletService.Setup(ap => ap.CreateWalletAsync(It.IsAny<WalletDetailsDto>())).Returns(Task.FromResult(true));

			var controllerInTest = new WalletController(mockWalletService.Object, _mapper);
			var result = await controllerInTest.CreateWallet(It.IsAny<CreateWalletRequest>());

			//Assert
			var okObjectResult = result as ObjectResult;
			Assert.Equal(expected: 201, okObjectResult.StatusCode);
		}

		[Fact]
		public async void For_DepositIntoWallet_Given_ValidWalletDetails_Should_Return_OkActionResult()
		{
			var mockWalletService = new Mock<IWalletService>();
			mockWalletService.Setup(ap => ap.DepositIntoWalletAsync(It.IsAny<DepositRequestDto>())).Returns(Task.FromResult(true));

			var controllerInTest = new WalletController(mockWalletService.Object, _mapper);
			var result = await controllerInTest.DepositIntoWallet(It.IsAny<DepositRequest>());

			//Assert
			var okObjectResult = result as ObjectResult;
			Assert.Equal(expected: 201, okObjectResult.StatusCode);
		}

		[Fact]
		public async void For_QueryBalance_Given_ValidWalletDetails_Should_Return_OkActionResult()
		{
			var walletDetails = new WalletDetailsDto { Balance = 500 };
			var mockWalletService = new Mock<IWalletService>();
			mockWalletService.Setup(ap => ap.GetWalletBalanceForUserAsync(It.IsAny<string>())).Returns(Task.FromResult(walletDetails));

			var controllerInTest = new WalletController(mockWalletService.Object, _mapper);
			var result = await controllerInTest.QueryBalance(It.IsAny<string>());

			//Assert
			var okObjectResult = result.Result as OkObjectResult;
			Assert.Equal(expected: 200, okObjectResult.StatusCode);
		}
	}
}
