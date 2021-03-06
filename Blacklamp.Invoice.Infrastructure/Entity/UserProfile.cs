using Microsoft.AspNetCore.Identity;

namespace Blacklamp.Invoice.Infrastructure.Entity
{
	public class UserProfile : IdentityUser
	{
		public string FirstName { get; set; }
		public string Surname { get; set; }
		public string Address { get; set; }
		public string BusinessName { get; set; }
		public string DeviceId { get; set; }		
		public string BusinessLogoBase64String { get; set; }		
	}
}
