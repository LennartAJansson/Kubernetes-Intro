namespace Auth.Module.Features.Users.UpdateUser;

using FastEndpoints;

public sealed class UpdateUserSummary : Summary<UpdateUserEndpoint>
{
  public UpdateUserSummary()
  {
    //TODO: Add Swagger properties here
    Summary = "Summary text goes here...";
    Description = "Description text goes here...";
  }
}