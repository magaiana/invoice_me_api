using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using BW.Assessment.Infrastructure.Models;
using BW.Assessment.Core.Models;

namespace BW.Assessment.Infrastructure.Persistence
{
	public class AssessmentDbContext : IdentityDbContext<IdentityUser>
	{
		public AssessmentDbContext(DbContextOptions<AssessmentDbContext> options) : base(options)
		{ }

		public DbSet<WalletDetails> WalletDetails { get; set; }
		public DbSet<Stock> Stocks { get; set; }
		public DbSet<Product> Products { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Product>().HasData(
				new Product
				{
					Id = 1,
					Description = "Laptop",
					Barcode = "12221379247",
					Price = 100
				},
				new Product
				{
					Id = 2,
					Description = "TV",
					Barcode = "7898797978978",
					Price = 200
				}, new Product
				{
					Id = 3,
					Description = "Desk",
					Barcode = "4354554543534",
					Price = 300
				}

			);

			modelBuilder.Entity<Stock>().HasData(
				new Stock
				{
					Id = 1,
					Description = "Laptop Stock",
					ProductId = 1,
					Quantity = 50
				},
				new Stock
				{
					Id = 2,
					Description = "TV Stock",
					ProductId = 2,
					Quantity = 40
				}, new Stock
				{
					Id = 3,
					Description = "Desk Stock",
					ProductId = 3,
					Quantity = 66
				}
			);
		}
	}


}
