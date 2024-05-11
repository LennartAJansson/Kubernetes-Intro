namespace Auth.Module.Features.Users.ResetPassword;

using FastEndpoints;

public sealed class ResetPasswordSummary
  : Summary<ResetPasswordEndpoint>
{
  public ResetPasswordSummary()
  {
    //TODO: Add Swagger properties here
    Summary = "Summary text goes here...";
    Description = "Description text goes here...";
  }
}