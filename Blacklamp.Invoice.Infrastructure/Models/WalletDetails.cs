using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blacklamp.Invoice.Infrastructure.Models
{
	public class WalletDetails
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int WalletId { get; set; }

		public string UserId { get; set; }

		public decimal Balance { get; set; }        
    }
}
