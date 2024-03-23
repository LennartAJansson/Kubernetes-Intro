namespace BuildVersionsApi.Features.Persistance.Context;

using BuildVersionsApi.Features.Model;

using Microsoft.EntityFrameworkCore;

public interface IBuildVersionsDbContext : IDbContext
{
  DbSet<BuildVersion> People { get; }

  void Migrate();
}