namespace Auth.Module.Features.Users.GetUsers;

using System.Threading.Tasks;

using Auth.Module.Model;

using FastEndpoints;

public sealed class GetUsersEndpoint(IUserService service)
  : EndpointWithoutRequest<GetUsersResponse, GetUsersMapper>
{
  public override void Configure()
  {
    Get("/auth/users");
    Policies("AdminPolicy");
  }

  public override async Task HandleAsync(CancellationToken c)
  {
    IEnumerable<AuthUser> users = await service.GetUsers();

    if (users is not null && users.Any())
    {
      await SendAsync(Map.FromEntity(users), 200, c);
    }
    else
    {
      await SendNotFoundAsync(c);
    }
  }
}