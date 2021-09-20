using System.Threading.Tasks;
using Blacklamp.Invoice.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Blacklamp.Invoice.Infrastructure.Persistence.Repository
{
	public class ShopRepository : IShopRepository
	{
		private readonly AssessmentDbContext _dbContext;
		public ShopRepository(AssessmentDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<Stock> GetStock(int productId)
		{
			var stock = await _dbContext.Stocks.FirstOrDefaultAsync(x => x.Product.Id == productId);
			return stock;
		}

		public async Task<bool> UpdateStock(Stock stock)
		{
			_dbContext.Entry(stock).State = EntityState.Modified;
			var changes = await _dbContext.SaveChangesAsync();
			return changes > 0;
		}
	}
}
