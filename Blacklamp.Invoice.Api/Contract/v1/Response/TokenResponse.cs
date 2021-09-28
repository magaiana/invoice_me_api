namespace Blacklamp.Invoice.Authentication.Api.Contract.v1.Response
{
	public record TokenResponse(string Id, string Username,
		string Email,
		string Token,
		bool IsActive, bool IsEmailComfirmed, bool IsPhoneNuberConfirmed);
}
