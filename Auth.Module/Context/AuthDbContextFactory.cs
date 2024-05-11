namespace Auth.Module.Context;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

/*
Add-Migration InitialAuth -Context AuthDbContext -Project Auth.Module -StartupProject Auth.Module
Add-Migration ExtendedUser -Context AuthDbContext -Project Auth.Module -StartupProject Auth.Module
Update-Database -Context AuthDbContext -Project Auth.Module -StartupProject Auth.Module
*/

internal sealed class AuthDbContextFactory : IDesignTimeDbContextFactory<AuthDbContext>
{
  public AuthDbContext CreateDbContext(string[] args)
  {
    IConfiguration configuration = new ConfigurationBuilder()
        .AddUserSecrets<AuthDbContext>()
        .Build();

    string? connectionString = configuration.GetConnectionString("AuthDb")
      ?? throw new ArgumentException("No connectionstring");

    ServerVersion serverVersion = ServerVersion.AutoDetect(connectionString);
    DbContextOptionsBuilder<AuthDbContext> optionsBuilder = new();
    _ = optionsBuilder.UseMySql(connectionString, serverVersion)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();

    return new AuthDbContext(optionsBuilder.Options);
  }
}