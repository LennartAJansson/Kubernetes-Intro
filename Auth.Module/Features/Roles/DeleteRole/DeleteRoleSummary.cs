namespace Auth.Module.Features.Roles.DeleteRole;

using FastEndpoints;

public sealed class DeleteRoleSummary
  : Summary<DeleteRoleEndpoint>
{
  public DeleteRoleSummary()
  {
    //TODO: Add Swagger properties here
    Summary = "Summary text goes here...";
    Description = "Description text goes here...";
  }
}