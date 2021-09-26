using System.ComponentModel.DataAnnotations.Schema;

namespace Blacklamp.Invoice.Core.Dtos
{
	public class Stock
	{
		public int Id { get; set; }
		public int Quantity { get; set; }
		public int ProductId { get; set; }

		[ForeignKey("ProductId")]
		public Product Product { get; set; }
		public string Description { get; set; }
	}
}
