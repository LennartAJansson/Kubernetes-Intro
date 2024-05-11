namespace Auth.Module.Features.Users.AssignRole;

using System.Threading.Tasks;

using Auth.Module.Model;

using FastEndpoints;

public sealed class AssignRoleEndpoint(IUserService service)
  : EndpointWithoutRequest<AssignRoleResponse, AssignRoleMapper>
{
  public override void Configure()
  {
    Post("/auth/roles/{role}/assign/{email}");
    Policies("AdminPolicy");
  }

  public override async Task HandleAsync(CancellationToken c)
  {
    //TODO: Put in a request object
    string role = Route<string>("role")!;
    string email = Route<string>("email")!;

    AuthUser user = await service.AssignRole(email, role);

    if (user is not null)
    {
      await SendAsync(Map.FromEntity(user), 200, c);
    }
    else
    {
      await SendNotFoundAsync(c);
    }
  }
}