namespace BW.Assessment.Wallet.Core.Models
{
	public class WithdrawalRequestDto
    {
        public string UserId { get; set; }

        public decimal Amount { get; set; }        

        public int WalletId { get; set; }
    }
}
