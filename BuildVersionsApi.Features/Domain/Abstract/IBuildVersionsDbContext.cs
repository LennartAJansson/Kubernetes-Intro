namespace BuildVersionsApi.Features.Domain.Abstract;

using BuildVersionsApi.Features.Domain.Model;

using Microsoft.EntityFrameworkCore;

public interface IBuildVersionsDbContext : IDbContext
{
    DbSet<BuildVersion> People { get; }

    void Migrate();
}