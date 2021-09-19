using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BW.Wallet.Wallet.Api.Contract.v1.Request
{
	public class DepositRequest
	{
		public string UserId { get; set; }
		public int WalletId { get; set; }
		public decimal Amount { get; set; }
	}
}
