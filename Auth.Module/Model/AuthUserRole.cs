namespace Auth.Module.Model;

using Microsoft.AspNetCore.Identity;

public class AuthUserRole : IdentityUserRole<Guid>
{
  public virtual AuthUser? User { get; set; }
  public virtual AuthRole? Role { get; set; }
}