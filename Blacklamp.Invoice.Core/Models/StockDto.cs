﻿namespace Blacklamp.Invoice.Core.Models
{
	public class StockDto
	{
		public int Id { get; set; }
		public int Quantity { get; set; }
		public int ProductId { get; set; }
		public string Description { get; set; }
	}
}
