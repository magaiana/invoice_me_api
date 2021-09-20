namespace Blacklamp.Invoice.Core.Models
{
	public class WalletDetailsDto
	{
		public int WalletId { get; set; }

		public string UserId { get; set; }

		public decimal Balance { get; set; }
	}
}
