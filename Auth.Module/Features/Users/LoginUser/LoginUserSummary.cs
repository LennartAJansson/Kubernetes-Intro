namespace Auth.Module.Features.Users.LoginUser;

using FastEndpoints;

public sealed class LoginUserSummary : Summary<LoginUserEndpoint>
{
  public LoginUserSummary()
  {
    //TODO: Add Swagger properties here
    Summary = "Summary text goes here...";
    Description = "Description text goes here...";
  }
}