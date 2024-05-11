namespace Auth.Module.Features.Users.RevokeRole;

using System.Threading.Tasks;

using FastEndpoints;

public sealed class RevokeRoleEndpoint(IUserService service)
  : EndpointWithoutRequest<RevokeRoleResponse, RevokeRoleMapper>
{
  public override void Configure()
  {
    Post("/auth/roles/{role}/revoke/{email}");
    Policies("AdminPolicy");
  }

  public override async Task HandleAsync(CancellationToken c)
  {
    string role = Route<string>("role")!;
    string email = Route<string>("email")!;

    Model.AuthUser? user = await service.RevokeRole(email, role);

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