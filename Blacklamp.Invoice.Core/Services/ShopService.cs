using System.Threading.Tasks;
using AutoMapper;
using Blacklamp.Invoice.Core.Models;
using Blacklamp.Invoice.Infrastructure.Models;
using Blacklamp.Invoice.Infrastructure.Persistence.Repository;

namespace Blacklamp.Invoice.Core.Services
{
	public class ShopService : IShopService
	{
		private readonly IMapper _mapper;
		private readonly IShopRepository _shopRepository;
		public ShopService(IShopRepository shopRepository, IMapper mapper)
		{
			_mapper = mapper;
			_shopRepository = shopRepository;
		}

		public async Task<StockDto> GetStock(int productId)
		{
			var stock = await _shopRepository.GetStock(productId);
			if (stock != null)
			{
				return _mapper.Map<Stock, StockDto>(stock);
			}
			return null;
		}

		public async Task<bool> UpdateStock(StockDto stockDto)
		{
			var stock = await _shopRepository.GetStock(stockDto.ProductId);
			if (stock != null)
			{
				stock.Quantity = stockDto.Quantity;
				return await _shopRepository.UpdateStock(stock);				
			}
			return false;
		}
	}
}
