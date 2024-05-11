namespace Auth.Module.Features.Users.LoginUser;

using System.Threading.Tasks;

using Auth.Module.Model;

using FastEndpoints;

//OKLAR
public sealed class LoginUserEndpoint(IUserService service)
  : Endpoint<LoginUserRequest, LoginUserResponse, LoginUserMapper>
{
  public override void Configure()
  {
    Post("/auth/users/login");
    AllowAnonymous();
  }

  public override async Task HandleAsync(LoginUserRequest r, CancellationToken c)
  {
    AuthLogin response = await service.LoginUser(r.Email, r.Password);

    if (response is not null)
    {
      await SendAsync(Map.FromEntity(response), 200, c);
    }
    else
    {
      await SendNotFoundAsync(c);
    }
  }
}