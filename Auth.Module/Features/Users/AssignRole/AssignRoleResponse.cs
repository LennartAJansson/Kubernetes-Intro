namespace Auth.Module.Features.Users.AssignRole;

public sealed class AssignRoleResponse
{
  public string Email { get; set; }
  public IEnumerable<string> Roles { get; set; }
}