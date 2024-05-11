namespace Auth.Module.Features.Roles.DeleteRole;

using Auth.Module.Model;

using FastEndpoints;

public sealed class DeleteRoleMapper
  : ResponseMapper<DeleteRoleResponse, AuthRole>
{
  public override DeleteRoleResponse FromEntity(AuthRole e) => new()
  {
    Role = e.Name!
  };
}