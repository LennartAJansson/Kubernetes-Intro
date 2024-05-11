namespace Auth.Module.Features.Users.ChangePassword;

using System.Threading.Tasks;

using Auth.Module.Model;

using FastEndpoints;

internal sealed class ChangePasswordEndpoint(IUserService service)
  : Endpoint<ChangePasswordRequest, ChangePasswordResponse, ChangePasswordMapper>
{
  public override void Configure()
  {
    Put("/auth/users/password");
    AllowAnonymous();
  }

  public override async Task HandleAsync(ChangePasswordRequest r, CancellationToken c)
  {
    if (User is null || User.Identity is null)
    {
      await SendUnauthorizedAsync(c);
      return;
    }

    string email = User.Identity.Name!;

    AuthUser? user = await service.ChangePassword(email, r.OldPassword, r.NewPassword, r.ConfirmPassword);

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