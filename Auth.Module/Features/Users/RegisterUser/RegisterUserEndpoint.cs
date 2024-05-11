namespace Auth.Module.Features.Users.RegisterUser;

using System.Threading.Tasks;

using FastEndpoints;

public sealed class RegisterUserEndpoint(IUserService service)
  : Endpoint<RegisterUserRequest, RegisterUserResponse>
{
  public override void Configure()
  {
    Post("/auth/users/register");
    AllowAnonymous();
  }

  public override async Task HandleAsync(RegisterUserRequest r, CancellationToken c)
  {
    string link = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}";
    string url = await service.RegisterUser(r.Firstname, r.Lastname, r.Email, r.Phonenumber, r.Password, r.ConfirmPassword, link);

    if (!string.IsNullOrWhiteSpace(url))
    {
      await SendAsync(new RegisterUserResponse { Url = url }, 200, c);
    }
    else
    {
      await SendNotFoundAsync(c);
    }
  }
}