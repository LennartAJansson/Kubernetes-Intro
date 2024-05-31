namespace Auth.Module.Features.Users.ChangePassword;

using FastEndpoints;

internal class ChangePasswordSummary
  : Summary<ChangePasswordEndpoint>
{
  public ChangePasswordSummary()
  {
    //TODO: Add Swagger properties here
    Summary = "Summary text goes here...";
    Description = "Description text goes here...";
  }
}