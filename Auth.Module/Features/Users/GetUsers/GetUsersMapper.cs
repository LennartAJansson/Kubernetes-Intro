namespace Auth.Module.Features.Users.GetUsers;

using System.Collections.Generic;

using Auth.Module.Model;

using FastEndpoints;

public sealed class GetUsersMapper
  : ResponseMapper<GetUsersResponse, IEnumerable<AuthUser>>
{
  public override GetUsersResponse FromEntity(IEnumerable<AuthUser> e) => new()
  {
    Users = e.Select(u => u.Email!),
  };
}