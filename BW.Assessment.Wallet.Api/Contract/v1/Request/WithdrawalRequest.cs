namespace BW.Wallet.Wallet.Api.Contract.v1.Request
{
	public class WithdrawalRequest
	{
		public string UserId { get; set; }
		public int WalletId { get; set; }
		public  decimal Amount { get; set; }
	}
}
