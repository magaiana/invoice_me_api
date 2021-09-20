using System;
using System.ComponentModel.DataAnnotations;

namespace BW.Assessment.Shop.Api.Contract.v1.Request
{
	public class AddStockRequest
	{
		[Required]
		public int ProductId { get; set; }

		[Required]
		public string Description { get; set; }

		[Required]
		[Range(0, 999999)]
		public int Quantity { get; set; }
	}
}
