namespace Blacklamp.Invoice.Authentication.Api.Contract.v1.Response
{
	public record TokenResponse(string Username, string Email, string Token, bool IsActive);
}
