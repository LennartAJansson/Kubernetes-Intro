namespace Auth.Module.Features.Users.UpdateUser;

using Auth.Module.Model;

using FastEndpoints;

public sealed class UpdateUserMapper : Mapper<UpdateUserRequest, UpdateUserResponse, AuthUser>
{
  public override UpdateUserResponse FromEntity(AuthUser e) => new()
  {
    Email = e.Email,
    Firstname = e.Firstname!,
    Lastname = e.Lastname!,
    PhoneNumber = e.PhoneNumber!
  };
}