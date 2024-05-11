namespace Auth.Module.Features.Users.AcknowledgePassword;

using FastEndpoints;

public sealed class AcknowledgePasswordSummary
  : Summary<AcknowledgePasswordEndpoint>
{
  public AcknowledgePasswordSummary()
  {
    //TODO: Add Swagger properties here
    Summary = "Summary text goes here...";
    Description = "Description text goes here...";
  }
}