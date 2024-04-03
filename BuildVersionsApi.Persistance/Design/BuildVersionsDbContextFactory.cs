namespace BuildVersionsApi.Persistance.Design;

using BuildVersionsApi.Persistance.Context;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

/*
This Design Time class is used to create migrations and update the database.
It requires following User Secrets to be set on this assembly:
{
  "ConnectionStrings": {
    "BuildVersionsDb": "Server=yourhost;Port=0000;User=username;Password=password;Database=buildversionsdata;"
  }
}

From Package Manager Console execute:
Add-Migration -Name your-migration-name -Context BuildVersionsDbContext -Project BuildVersionsApi.Persistance -StartupProject BuildVersionsApi.Persistance -Namespace Migrations
Update-Database -Context BuildVersionsDbContext -Project BuildVersionsApi.Persistance -StartupProject BuildVersionsApi.Persistance
*/

public sealed class BuildVersionsDbContextFactory
  : IDesignTimeDbContextFactory<BuildVersionsDbContext>
{
  public BuildVersionsDbContext CreateDbContext(string[] args)
  {
    IConfiguration configuration = new ConfigurationBuilder()
        .AddUserSecrets<BuildVersionsDbContext>()
        .Build();

    string? connectionString = configuration.GetConnectionString("BuildVersionsDb");
    ServerVersion serverVersion = ServerVersion.AutoDetect(connectionString);
    DbContextOptionsBuilder<BuildVersionsDbContext> optionsBuilder = new();
    _ = optionsBuilder.UseMySql(connectionString, serverVersion)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors();

    return new BuildVersionsDbContext(optionsBuilder.Options);
  }
}