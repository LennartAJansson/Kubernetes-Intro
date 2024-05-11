namespace Auth.Module.Features.Users.RevokeRole;

using FastEndpoints;

public sealed class RevokeRoleSummary
  : Summary<RevokeRoleEndpoint>
{
  public RevokeRoleSummary()
  {
    //TODO: Add Swagger properties here
    Summary = "Summary text goes here...";
    Description = "Description text goes here...";
  }
}