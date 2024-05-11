namespace Auth.Module.Features.Users.GetUsers;

using System.Collections.Generic;

public sealed class GetUsersResponse
{
  //TODO: Make a useful response value
  public IEnumerable<string> Users { get; set; }
}