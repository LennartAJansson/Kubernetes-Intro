namespace Auth.Module.Features.Users.ChangePassword;

internal sealed class ChangePasswordRequest
{
  public string OldPassword { get; internal set; }
  public string NewPassword { get; internal set; }
  public string ConfirmPassword { get; internal set; }
}