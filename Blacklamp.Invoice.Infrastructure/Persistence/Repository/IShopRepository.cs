using System.Threading.Tasks;
using Blacklamp.Invoice.Core.Models;

namespace Blacklamp.Invoice.Infrastructure.Persistence.Repository
{
	public interface IShopRepository
	{
		Task<Stock> GetStock(int productId);
		Task<bool> UpdateStock(Stock stock);
	} 
}
