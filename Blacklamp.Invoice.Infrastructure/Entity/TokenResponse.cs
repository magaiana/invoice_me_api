namespace Blacklamp.Invoice.Infrastructure.Entity
{
	public class TokenResponse : Entity
	{
		public string Username { get; set; }
		public string Email { get; set; }
		public string Token { get; set; }
		public bool IsActive { get; set; }
	}
}
