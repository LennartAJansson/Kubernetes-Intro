namespace Auth.Module.Features.Users.RegisterUser;

public sealed class RegisterUserRequest
{
  public string Firstname { get; init; }
  public string Lastname { get; init; }
  public string Email { get; init; }
  public string Phonenumber { get; init; }
  public string Password { get; init; }
  public string ConfirmPassword { get; init; }
}