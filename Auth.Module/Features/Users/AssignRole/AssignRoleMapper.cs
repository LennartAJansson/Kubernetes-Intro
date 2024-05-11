namespace Auth.Module.Features.Users.AssignRole;

using Auth.Module.Model;

using FastEndpoints;

public sealed class AssignRoleMapper
  : ResponseMapper<AssignRoleResponse, AuthUser>
{
  public override AssignRoleResponse FromEntity(AuthUser e) => new()
  {
    Email = e.Email!,
    Roles = e.UserRoles.Select(r => r.Role!.Name)!
  };
}