namespace Auth.Module.Features.Roles.GetRoles;

using Auth.Module.Model;

using FastEndpoints;

public sealed class GetRolesMapper
  : ResponseMapper<GetRolesResponse, IEnumerable<AuthRole>>
{
  public override GetRolesResponse FromEntity(IEnumerable<AuthRole> e) => new()
  {
    Roles = e.Select(r => r.Name!)
  };
}