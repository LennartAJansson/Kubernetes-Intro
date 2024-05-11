namespace BuildVersionsApi.Persistance.Context;

using System.Reflection;

using BuildVersionsApi.Domain.Model;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public sealed class BuildVersionsDbContext(DbContextOptions<BuildVersionsDbContext> options)
  : DbContext(options)
{
  public DbSet<BuildVersion> BuildVersions => Set<BuildVersion>();

  public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
  private ILogger<BuildVersionsDbContext>? logger;

  protected override void OnModelCreating(ModelBuilder modelBuilder)
    => modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    _ = optionsBuilder
      .UseLoggerFactory(loggerFactory);
    logger = loggerFactory.CreateLogger<BuildVersionsDbContext>();
  }

  public Task EnsureDbExists()
  {
    if (Database.GetPendingMigrations().Any())
    {
      logger?.LogInformation("Adding {count} migrations", Database.GetPendingMigrations().Count());
      Database.Migrate();
    }
    else
    {
      logger?.LogInformation("Migrations up to date");
    }

    return Task.CompletedTask;
  }
}