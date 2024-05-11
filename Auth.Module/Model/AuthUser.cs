namespace Auth.Module.Model;

using System;

using Microsoft.AspNetCore.Identity;

public sealed class AuthUser : IdentityUser<Guid>
{
  public string? Firstname { get; set; }
  public string? Lastname { get; set; }
  public string? ProfileImage { get; set; }
  public ICollection<AuthUserRole> UserRoles { get; set; } = [];
}