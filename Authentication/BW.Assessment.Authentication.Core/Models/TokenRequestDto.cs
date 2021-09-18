namespace BW.Assessment.Authentication.Core.Models
{
	public class TokenRequestDto
	{
        public string Username { get; set; }

        public string Password { get; set; }

        public string SomeInternalProperty { get; set; }
    }
}
