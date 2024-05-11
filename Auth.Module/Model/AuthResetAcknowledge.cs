namespace Auth.Module.Model;

public sealed class AuthResetAcknowledge(string email, string password, string token)
{
  public string Email { get; set; } = email;
  public string Password { get; set; } = password;
  public string Token { get; set; } = token;
}