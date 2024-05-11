namespace Auth.Module.Model;

using System;

using Microsoft.AspNetCore.Identity;

public sealed class AuthRole : IdentityRole<Guid>
{
  public ICollection<AuthUserRole> UserRoles { get; set; } = [];
}