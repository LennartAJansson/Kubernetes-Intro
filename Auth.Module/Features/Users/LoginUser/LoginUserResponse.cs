namespace Auth.Module.Features.Users.LoginUser;

public sealed class LoginUserResponse
{
  //Guid id, string? userName, IEnumerable<string> roles, string jwt, string refresh
  public Guid Id { get; set; }

  public string Firstname { get; set; }
  public string Lastname { get; set; }
  public string? Email { get; set; }
  public IEnumerable<string> Roles { get; set; }
  public string JwtToken { get; set; }
  public string RefreshToken { get; set; }
}