namespace Auth.Module.Features.Users.LoginUser;

using Auth.Module.Model;

using FastEndpoints;

public sealed class LoginUserMapper : Mapper<LoginUserRequest, LoginUserResponse, AuthLogin>
{
  public override LoginUserResponse FromEntity(AuthLogin e) => new()
  {
    Id = e.Id,
    Firstname = e.Firstname,
    Lastname = e.Lastname,
    Email = e.Email,
    Roles = e.Roles,
    JwtToken = e.JwtToken,
    RefreshToken = e.RefreshToken
  };
}