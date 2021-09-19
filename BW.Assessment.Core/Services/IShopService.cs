using System.Threading.Tasks;
using BW.Assessment.Core.Models;

namespace BW.Assessment.Core.Services
{
	public interface IShopService
	{
		Task<StockDto> GetStock(int productId);
		Task<bool> UpdateStock(StockDto stock);
	}
}
