namespace Auth.Module.Features.Roles.CreateRole;

using Auth.Module.Model;

using FastEndpoints;

public sealed class CreateRoleMapper
  : ResponseMapper<CreateRoleResponse, AuthRole>
{
  public override CreateRoleResponse FromEntity(AuthRole e) => new()
  {
    Role = e.Name!
  };
}