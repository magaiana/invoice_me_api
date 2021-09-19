using System.ComponentModel.DataAnnotations.Schema;

namespace BW.Assessment.Core.Models
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
