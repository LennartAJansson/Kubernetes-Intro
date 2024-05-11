namespace Auth.Module.Features.Roles.CreateRole;

using System.Threading.Tasks;

using Auth.Module.Model;

using FastEndpoints;

public sealed class CreateRoleEndpoint(IRoleService service)
  : EndpointWithoutRequest<CreateRoleResponse, CreateRoleMapper>
{
  public override void Configure()
  {
    Post("/auth/roles/create/{name}");
    Policies("AdminPolicy");
  }

  public override async Task HandleAsync(CancellationToken c)
  {
    //TODO: Put in a request object
    string name = Route<string>("name")!;

    AuthRole? role = await service.CreateRole(name);

    if (role is not null)
    {
      await SendAsync(Map.FromEntity(role), 200, c);
    }
    else
    {
      await SendNotFoundAsync(c);
    }
  }
}