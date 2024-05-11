namespace Auth.Module.Features.Users.LoginUser;

public sealed class LoginUserRequest
{
  public string Email { get; set; }
  public string Password { get; set; }
}