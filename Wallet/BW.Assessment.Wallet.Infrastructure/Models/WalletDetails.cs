using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BW.Assessment.Wallet.Core.Models
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
