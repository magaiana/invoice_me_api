using System.Threading.Tasks;
using Blacklamp.Invoice.Core.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Blacklamp.Invoice.Infrastructure.Persistence.Repository
{
	public class ShopRepository : IShopRepository
	{
		private readonly InvoiceDbContext _dbContext;
		public ShopRepository(InvoiceDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<Stock> GetStock(int productId)
		{
			//var stock = await _dbContext.Stocks.FirstOrDefaultAsync(x => x.Product.Id == productId);
			return null;
		}

		public async Task<bool> UpdateStock(Stock stock)
		{
			_dbContext.Entry(stock).State = EntityState.Modified;
			var changes = await _dbContext.SaveChangesAsync();
			return changes > 0;
		}
	}
}
