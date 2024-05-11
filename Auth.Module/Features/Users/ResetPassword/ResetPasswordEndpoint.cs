namespace Auth.Module.Features.Users.ResetPassword;

using System.Threading.Tasks;

using FastEndpoints;

public sealed class ResetPasswordEndpoint(IUserService service)
  : Endpoint<ResetPasswordRequest, ResetPasswordResponse>
{
  public override void Configure()
  {
    Post("/auth/password/reset");
    AllowAnonymous();
  }

  public override async Task HandleAsync(ResetPasswordRequest r, CancellationToken c)
  {
    string link = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}";
    string url = await service.ResetPassword(r.Email, r.Password, r.ConfirmPassword, link);

    if (!string.IsNullOrWhiteSpace(url))
    {
      await SendAsync(new ResetPasswordResponse { Url = url }, 200, c);
    }
    else
    {
      await SendNotFoundAsync(c);
    }
  }
}