using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BW.Wallet.Authentication.Core.Models
{
	public class WalletDetailsDto
	{
		public int WalletId { get; set; }

		public string UserId { get; set; }

		public decimal Balance { get; set; }
	}
}
