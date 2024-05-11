namespace Auth.Module.Features.Users.RevokeRole;

public sealed class RevokeRoleResponse
{
  public string Email { get; set; }
  public IEnumerable<string> Roles { get; set; }
}