namespace Auth.Module.Features.Roles.CreateRole;

using FastEndpoints;

public sealed class CreateRoleSummary
  : Summary<CreateRoleEndpoint>
{
  public CreateRoleSummary()
  {
    //TODO: Add Swagger properties here
    Summary = "Summary text goes here...";
    Description = "Description text goes here...";
  }
}