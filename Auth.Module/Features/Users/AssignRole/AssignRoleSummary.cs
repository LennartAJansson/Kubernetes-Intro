namespace Auth.Module.Features.Users.AssignRole;

using FastEndpoints;

public sealed class AssignRoleSummary
  : Summary<AssignRoleEndpoint>
{
  public AssignRoleSummary()
  {
    //TODO: Add Swagger properties here
    Summary = "Summary text goes here...";
    Description = "Description text goes here...";
  }
}