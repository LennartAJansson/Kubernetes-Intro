namespace Auth.Module.Features.Users.RevokeRole;

using Auth.Module.Model;

using FastEndpoints;

public sealed class RevokeRoleMapper
  : ResponseMapper<RevokeRoleResponse, AuthUser>
{
  public override RevokeRoleResponse FromEntity(AuthUser e) => new()
  {
    Email = e.Email,
    Roles = e.UserRoles.Select(r => r.Role!.Name)!
  };
}