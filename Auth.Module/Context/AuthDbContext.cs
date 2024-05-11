namespace Auth.Module.Context;

using Auth.Module.Model;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

//https://stackoverflow.com/questions/51004516/net-core-2-1-identity-get-all-users-with-their-associated-roles/51005445#51005445
//https://github.com/aspnet/Identity/issues/1361#issuecomment-348863959
internal sealed class AuthDbContext(DbContextOptions<AuthDbContext> options)
  : IdentityDbContext<AuthUser, AuthRole, Guid,
    IdentityUserClaim<Guid>, AuthUserRole,
    IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>,
    IdentityUserToken<Guid>>(options)
{
  protected override void OnModelCreating(ModelBuilder builder)
  {
    base.OnModelCreating(builder);
    //Add mapping for Many-to-Many for relation roles-users
    _ = builder.Entity<AuthUserRole>(userRole =>
    {
      _ = userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

      _ = userRole.HasOne(ur => ur.Role)
          .WithMany(r => r.UserRoles)
          .HasForeignKey(ur => ur.RoleId)
          .IsRequired();

      _ = userRole.HasOne(ur => ur.User)
          .WithMany(r => r.UserRoles)
          .HasForeignKey(ur => ur.UserId)
          .IsRequired();
    });
  }

  public void EnsureDbExists()
  {
    IEnumerable<string> migrations = Database.GetPendingMigrations();
    if (migrations.Any())
    {
      Database.Migrate();
    }
    EnsureBaseRolesExists();
  }

  private void EnsureBaseRolesExists()
  {
    bool needsToSave = false;

    if (!Roles.Where(r => r.Name == "User").Any())
    {
      _ = Roles.Add(new AuthRole { Id = Guid.NewGuid(), Name = "User", NormalizedName = "USER", ConcurrencyStamp = Guid.NewGuid().ToString() });
      needsToSave = true;
    }

    if (!Roles.Where(r => r.Name == "Admin").Any())
    {
      _ = Roles.Add(new AuthRole { Id = Guid.NewGuid(), Name = "Admin", NormalizedName = "ADMIN", ConcurrencyStamp = Guid.NewGuid().ToString() });
      needsToSave = true;
    }

    if (!Roles.Where(r => r.Name == "SuperAdmin").Any())
    {
      _ = Roles.Add(new AuthRole { Id = Guid.NewGuid(), Name = "SuperAdmin", NormalizedName = "SUPERADMIN", ConcurrencyStamp = Guid.NewGuid().ToString() });
      needsToSave = true;
    }
    if (needsToSave)
    {
      _ = SaveChanges();
    }
  }
}