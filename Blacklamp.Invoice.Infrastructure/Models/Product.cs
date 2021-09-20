namespace Blacklamp.Invoice.Core.Models
{
	public class Product
	{
		public int Id { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		public string Barcode { get; set; }
	}
}
