namespace BuildVersionsApi.Domain.Abstract;

using BuildVersionsApi.Domain.Model;

using Microsoft.EntityFrameworkCore;

public interface IBuildVersionsDbContext : IDbContext
{
  DbSet<BuildVersion> People { get; }

  void Migrate();
}