namespace Auth.Module.Features.Users.ChangePassword;

using Auth.Module.Model;

using FastEndpoints;

internal sealed class ChangePasswordMapper : Mapper<ChangePasswordRequest, ChangePasswordResponse, AuthUser>
{
  public override ChangePasswordResponse FromEntity(AuthUser e) => new()
  {
    Email = e.Email!
  };
}