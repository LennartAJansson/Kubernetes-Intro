namespace Auth.Module.Features.Users.AcknowledgeUser;

using Auth.Module.Model;

using FastEndpoints;

public sealed class AcknowledgeUserMapper
  : ResponseMapper<AcknowledgeUserResponse, AuthUser>
{
  public override AcknowledgeUserResponse FromEntity(AuthUser e) => new()
  {
    Id = e.Id,
    Email = e.Email,
    PhoneNumber = e.PhoneNumber,
    Roles = e.UserRoles.Select(e => e.Role.Name)
  };
}