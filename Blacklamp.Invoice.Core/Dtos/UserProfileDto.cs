namespace Blacklamp.Invoice.Core.Dtos
{
	public class UserProfileDto
	{
		public string Id { get; set; }
		public string FirstName { get; set; }
		public string Surname { get; set; }
		public string Address { get; set; }
		public string Email { get; set; }
		public string BusinessName { get; set; }
		public string BusinessLogoBase64String { get; set; }

		public string UserName { get; set; }
		public string Password { get; set; }
	}
}
