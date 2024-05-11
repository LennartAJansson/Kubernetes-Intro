namespace BuildVersionsApi.Persistance.Service;

using System.Linq.Expressions;

using BuildVersionsApi.Domain.Abstract;
using BuildVersionsApi.Domain.Model;
using BuildVersionsApi.Persistance.Context;

using Microsoft.EntityFrameworkCore;

public sealed class StorageService(BuildVersionsDbContext context)
  : IStorageService
{
  public async Task<BuildVersion?> CreateProject(BuildVersion buildVersion, CancellationToken cancellationToken)
  {
    if (await context.BuildVersions.AnyAsync(b =>
      b.ProjectName.Equals(buildVersion.ProjectName), cancellationToken))
    {
      return null;
    }
    _ = await context.AddAsync(buildVersion, cancellationToken);
    _ = await context.SaveChangesAsync(cancellationToken);

    return buildVersion;
  }

  public async Task<BuildVersion?> UpdateProject(BuildVersion buildVersion, CancellationToken cancellationToken)
  {
    if (!await context.BuildVersions.AnyAsync(b =>
      b.Id == buildVersion.Id, cancellationToken))
    {
      return null;
    }

    _ = context.Update(buildVersion);
    _ = await context.SaveChangesAsync(cancellationToken);

    return buildVersion;
  }

  public async Task<BuildVersion?> Delete(BuildVersion buildVersion, CancellationToken cancellationToken)
  {
    _ = context.Remove(buildVersion);
    _ = await context.SaveChangesAsync(cancellationToken);

    return buildVersion;
  }

  public async Task<BuildVersion?> GetById(int id, CancellationToken cancellationToken)
  {
    BuildVersion? model = await Get(cancellationToken, b =>
      b.Id == id);

    return model;
  }

  public async Task<BuildVersion?> GetByName(string projectName, CancellationToken cancellationToken)
  {
    BuildVersion? model = await Get(cancellationToken, b =>
      b.ProjectName.Equals(projectName));

    return model;
  }

  public async Task<BuildVersion?> Get(CancellationToken cancellationToken, Expression<Func<BuildVersion, bool>>? find = null)
  {
    IQueryable<BuildVersion> query = context.BuildVersions;

    if (find != null)
    {
      query = query.Where(b => !b.IsDeleted).Where(find);
    }

    BuildVersion? model = await query.SingleOrDefaultAsync(cancellationToken);

    return model;
  }

  public Task<IEnumerable<BuildVersion>> GetAll(CancellationToken cancellationToken)
    => Task.FromResult(context.BuildVersions.Where(b => !b.IsDeleted).AsEnumerable());
}