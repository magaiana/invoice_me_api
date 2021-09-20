using AutoMapper;
using BW.Assessment.Core.Models;
using BW.Assessment.Core.Services;
using BW.Assessment.Infrastructure.Models;
using BW.Assessment.Infrastructure.Persistence.Repository;
using BW.Assessment.Shop.Api.Utilities;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace BW.Assessment.Wallet.Tests
{
	public class ShopServiceTests
	{
		private static IMapper _mapper;

		public ShopServiceTests()
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
		public async void For_GetStock_Given_ValidProductId_Should_Return_Stock()
		{			
			var stock = new Stock { Quantity = 50, ProductId = 1 };			
			var mockShopRepository = new Mock<IShopRepository>();

			mockShopRepository.Setup(ap => ap.GetStock(It.IsAny<int>())).Returns(Task.FromResult(stock));

			var serviceInTest = new ShopService(mockShopRepository.Object, _mapper);
			var result = await serviceInTest.GetStock(It.IsAny<int>());

			//Assert
			Assert.Equal(expected: 50, actual: result.Quantity);
		}

		[Fact]
		public async void For_UpdateStock_Given_ValidDetails_Should_Update_Stock_Successfully()
		{
			var stock = new Stock { Quantity = 50, ProductId = 1 };
			var stockDto = new StockDto { Quantity = 50, ProductId = 1 };
			var mockShopRepository = new Mock<IShopRepository>();

			mockShopRepository.Setup(ap => ap.UpdateStock(stock)).Returns(Task.FromResult(true));
			mockShopRepository.Setup(ap => ap.GetStock(It.IsAny<int>())).Returns(Task.FromResult(stock));

			var serviceInTest = new ShopService(mockShopRepository.Object, _mapper);
			var result = await serviceInTest.UpdateStock(stockDto);

			//Assert
			Assert.True(result);
		}
	}
}
