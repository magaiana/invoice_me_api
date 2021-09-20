using System.Threading.Tasks;
using Blacklamp.Invoice.Core.Models;

namespace Blacklamp.Invoice.Core.Services
{
	public interface IShopService
	{
		Task<StockDto> GetStock(int productId);
		Task<bool> UpdateStock(StockDto stock);
	}
}
