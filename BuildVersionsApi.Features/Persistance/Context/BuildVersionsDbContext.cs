namespace BuildVersionsApi.Features.Persistance.Context;

using BuildVersionsApi.Features.Model;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;

public sealed class BuildVersionsDbContext : DbContext
{
  public DbSet<BuildVersion> BuildVersions => Set<BuildVersion>();

  public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
  private ILogger<BuildVersionsDbContext>? logger;

  public BuildVersionsDbContext(DbContextOptions<BuildVersionsDbContext> options)
  : base(options)
  { }

  protected override void OnModelCreating(ModelBuilder modelBuilder) => _ = modelBuilder.Entity<BuildVersion>().HasIndex("ProjectName");

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    _ = optionsBuilder.UseLoggerFactory(loggerFactory);
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

  public override int SaveChanges()
  {
    PreSaveChanges();

    return base.SaveChanges();
  }

  public override int SaveChanges(bool acceptAllChangesOnSuccess)
  {
    PreSaveChanges();

    return base.SaveChanges(acceptAllChangesOnSuccess);
  }

  public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
  {
    PreSaveChanges();

    return await base.SaveChangesAsync(cancellationToken);
  }

  public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
  {
    PreSaveChanges();

    return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
  }

  private void PreSaveChanges()
  {
    foreach (BaseLoggedEntity? history in ChangeTracker
        .Entries()
        .Where(e => e.Entity is BaseLoggedEntity)
        .Select(e => e.Entity as BaseLoggedEntity))
    {
      history!.Changed = DateTime.Now;
      history.Username = "";
      if (history.Created == DateTime.MinValue)
      {
        history.Created = DateTime.Now;
      }
    }
  }
}

