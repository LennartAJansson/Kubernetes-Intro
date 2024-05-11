namespace Auth.Module.Model;

public sealed class AuthUserAcknowledge(string email, string token)
{
  public string Email { get; set; } = email;
  public string Token { get; set; } = token;
}