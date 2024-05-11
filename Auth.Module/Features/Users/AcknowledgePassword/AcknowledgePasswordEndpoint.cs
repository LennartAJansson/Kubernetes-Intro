namespace Auth.Module.Features.Users.AcknowledgePassword;

using System.Threading.Tasks;

using Auth.Module.Model;

using FastEndpoints;

public sealed class AcknowledgePasswordEndpoint(IUserService service)
  : EndpointWithoutRequest<AcknowledgePasswordResponse, AcknowledgePasswordMapper>
{
  public override void Configure()
  {
    //TODO Change to use ?token={token}, right now it's using /{token}
    Get("/auth/password/acknowledge/{token}");
    AllowAnonymous();
  }

  public override async Task HandleAsync(CancellationToken c)
  {
    //TODO: Put in a request object
    string? token = Route<string>("token");

    AuthUser? user = await service.AcknowledgePassword(token);

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