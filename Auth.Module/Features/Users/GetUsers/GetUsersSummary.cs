namespace Auth.Module.Features.Users.GetUsers;

using FastEndpoints;

public sealed class GetUsersSummary
  : Summary<GetUsersEndpoint>
{
  public GetUsersSummary()
  {
    //TODO: Add Swagger properties here
    Summary = "Summary text goes here...";
    Description = "Description text goes here...";
  }
}