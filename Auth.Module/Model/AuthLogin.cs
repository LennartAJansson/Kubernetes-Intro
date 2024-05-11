namespace Auth.Module.Model;

using System;

public sealed class AuthLogin
  (Guid id, string firstname, string lastname, string? email, IEnumerable<string> roles, string jwtToken, string refreshToken)
{
  public Guid Id { get; private set; } = id;
  public string Firstname { get; set; } = firstname;
  public string Lastname { get; set; } = lastname;
  public string? Email { get; private set; } = email;
  public IEnumerable<string> Roles { get; private set; } = roles;
  public string JwtToken { get; private set; } = jwtToken;
  public string RefreshToken { get; private set; } = refreshToken;
}