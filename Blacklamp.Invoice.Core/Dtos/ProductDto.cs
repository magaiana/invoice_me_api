namespace Blacklamp.Invoice.Core.Dtos
{
	public class ProductDto
	{
		public int Id { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		public string Barcode { get; set; }
	}
}
