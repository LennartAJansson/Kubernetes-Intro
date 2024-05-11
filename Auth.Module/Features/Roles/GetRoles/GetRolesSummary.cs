namespace Auth.Module.Features.Roles.GetRoles;

using FastEndpoints;

public sealed class GetRolesSummary
  : Summary<GetRolesEndpoint>
{
  public GetRolesSummary()
  {
    //TODO: Add Swagger properties here
    Summary = "Summary text goes here...";
    Description = "Description text goes here...";
  }
}