namespace Auth.Module.Features.Roles.GetRoles;

using System.Threading.Tasks;

using Auth.Module.Model;

using FastEndpoints;

public sealed class GetRolesEndpoint(IRoleService service)
  : EndpointWithoutRequest<GetRolesResponse, GetRolesMapper>
{
  public override void Configure()
  {
    Get("/auth/roles");
    Policies("UserPolicy");
  }

  public override async Task HandleAsync(CancellationToken c)
  {
    IEnumerable<AuthRole> roles = await service.GetRoles();

    if (roles is not null && roles.Any())
    {
      await SendAsync(Map.FromEntity(roles), 200, c);
    }
    else
    {
      await SendNotFoundAsync(c);
    }
  }
}