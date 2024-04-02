namespace BuildVersionsApi.Persistance.Service;

using BuildVersionsApi.Domain.Abstract;
using BuildVersionsApi.Domain.Model;
using BuildVersionsApi.Persistance.Context;

using Microsoft.EntityFrameworkCore;

public sealed class StorageService(BuildVersionsDbContext context)
  : IStorageService
{
  public async Task<BuildVersion?> CreateProject(BuildVersion buildVersion, CancellationToken cancellationToken)
  {
    if (await context.BuildVersions.AnyAsync(b => b.ProjectName.Equals(buildVersion.ProjectName), cancellationToken))
    {
      return null;
    }
    _ = await context.AddAsync(buildVersion, cancellationToken);
    _ = await context.SaveChangesAsync(cancellationToken);

    return buildVersion;
  }

  public async Task<BuildVersion?> UpdateProject(BuildVersion buildVersion, CancellationToken cancellationToken)
  {
    if (!await context.BuildVersions.AnyAsync(b => b.Id == buildVersion.Id, cancellationToken))
    {
      return null;
    }

    _ = context.Update(buildVersion);
    _ = await context.SaveChangesAsync(cancellationToken);

    return buildVersion;
  }

  public async Task<BuildVersion?> GetById(int id, CancellationToken cancellationToken)
  {
    //HINT Handle soft delete by property IsDeleted in the entity class
    BuildVersion? model = await context.BuildVersions.SingleOrDefaultAsync(b =>
    b.Id == id && !b.IsDeleted, cancellationToken);

    return model;
  }

  public async Task<BuildVersion?> GetByName(string projectName, CancellationToken cancellationToken)
  {
    //HINT Handle soft delete by property IsDeleted in the entity class
    BuildVersion? model = await context.BuildVersions.SingleOrDefaultAsync(b
      => b.ProjectName.Equals(projectName) && !b.IsDeleted, cancellationToken);

    return model;
  }

  public Task<IEnumerable<BuildVersion>> GetAll(CancellationToken cancellationToken)
    //HINT Handle soft delete by property IsDeleted in the entity class
    => Task.FromResult(context.BuildVersions.Where(b => !b.IsDeleted).AsEnumerable());

  public async Task<BuildVersion?> Delete(BuildVersion buildVersion, CancellationToken cancellationToken)
  {
    //HINT Handle soft delete by property IsDeleted in the entity class
    //_ = context.Remove(buildVersion!);

    _ = await context.SaveChangesAsync(cancellationToken);

    return buildVersion;
  }
}