namespace Blacklamp.Invoice.Api.Contract.v1.Request
{
	public class SignupRequest
	{
		public string BusinessName { get; set; }
		public string FirstName { get; set; }
		public string Surname { get; set; }
		public string ContactNumber { get; set; }
		public string EmailAddress { get; set; }
		public string DeviceId { get; set; }		
	}
}
