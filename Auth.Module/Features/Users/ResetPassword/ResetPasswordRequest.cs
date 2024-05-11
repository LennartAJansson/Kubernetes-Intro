namespace Auth.Module.Features.Users.ResetPassword;

public sealed class ResetPasswordRequest
{
  public string Email { get; set; }
  public string Password { get; set; }
  public string ConfirmPassword { get; set; }
}