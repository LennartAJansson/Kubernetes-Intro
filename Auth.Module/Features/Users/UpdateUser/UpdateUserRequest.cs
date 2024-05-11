namespace Auth.Module.Features.Users.UpdateUser;

using System.Security.Claims;

using FastEndpoints;

public sealed class UpdateUserRequest
{
  [FromClaim(claimType: ClaimTypes.Email)]
  public string? Emailaddress { get; set; }

  public string Firstname { get; set; }
  public string Lastname { get; set; }
  public string PhoneNumber { get; set; }
}