namespace Auth.Module.Features.Users.RegisterUser;

using FastEndpoints;

public sealed class RegisterUserSummary
  : Summary<RegisterUserEndpoint>
{
  public RegisterUserSummary()
  {
    //TODO: Add Swagger properties here
    Summary = "Summary text goes here...";
    Description = "Description text goes here...";
  }
}