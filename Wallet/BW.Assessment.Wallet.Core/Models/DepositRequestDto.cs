namespace BW.Assessment.Wallet.Core.Models
{
	public class DepositRequestDto
    {
        public string UserId { get; set; }

        public decimal Amount { get; set; }        

        public string DepositMethod { get; set; }
    }
}
