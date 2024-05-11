namespace Auth.Module.Features.Roles.GetRoles;

public sealed class GetRolesResponse
{
  public IEnumerable<string>? Roles { get; set; }
}