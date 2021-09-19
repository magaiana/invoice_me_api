using System.Threading.Tasks;
using BW.Assessment.Core.Models;

namespace BW.Assessment.Infrastructure.Persistence.Repository
{
	public interface IShopRepository
	{
		Task<Stock> GetStock(int productId);
		Task<bool> UpdateStock(Stock stock);
	} 
}
