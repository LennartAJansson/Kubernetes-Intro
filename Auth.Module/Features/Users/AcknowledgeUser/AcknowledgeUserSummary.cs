namespace Auth.Module.Features.Users.AcknowledgeUser;

using FastEndpoints;

public sealed class AcknowledgeUserSummary
  : Summary<AcknowledgeUserEndpoint>
{
  public AcknowledgeUserSummary()
  {
    //TODO: Add Swagger properties here
    Summary = "Summary text goes here...";
    Description = "Description text goes here...";
  }
}