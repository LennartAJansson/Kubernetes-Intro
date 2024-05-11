namespace Auth.Module.Features.Users.AcknowledgePassword;

using Auth.Module.Model;

using FastEndpoints;

public sealed class AcknowledgePasswordMapper
  : ResponseMapper<AcknowledgePasswordResponse, AuthUser>
{
  public override AcknowledgePasswordResponse FromEntity(AuthUser e) => new()
  {
    Id = e.Id,
    Email = e.Email,
    PhoneNumber = e.PhoneNumber,
    Roles = e.UserRoles.Select(e => e.Role.Name)
  };
}