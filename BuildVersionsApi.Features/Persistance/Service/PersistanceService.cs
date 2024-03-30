namespace BuildVersionsApi.Features.Persistance.Service;

using BuildVersionsApi.Features.Domain.Abstract;
using BuildVersionsApi.Features.Domain.Model;
using BuildVersionsApi.Features.Persistance.Context;

using BuildVersionsApi.Features.Types;

using Microsoft.EntityFrameworkCore;

public sealed class PersistanceService(BuildVersionsDbContext context)
  : IPersistanceService
{
  public async Task<BuildVersion?> CreateProject(BuildVersion buildVersion)
  {
    if (await context.BuildVersions.AnyAsync(b => b.ProjectName.Equals(buildVersion.ProjectName)))
    {
      return null;
    }
    _ = context.Add(buildVersion);
    _ = await context.SaveChangesAsync();

    return buildVersion;
  }

  public async Task<BuildVersion?> UpdateProject(BuildVersion buildVersion)
  {
    if (!await context.BuildVersions.AnyAsync(b => b.Id == buildVersion.Id))
    {
      return null;
    }

    _ = context.Update(buildVersion);
    _ = await context.SaveChangesAsync();

    return buildVersion;
  }

  public async Task<BuildVersion?> GetById(int id)
  {
    BuildVersion? model = await context.BuildVersions.SingleOrDefaultAsync(b => b.Id == id && !b.IsDeleted);

    return model;
  }

  public async Task<BuildVersion?> GetByName(string projectName)
  {
    BuildVersion? model = await context.BuildVersions.SingleOrDefaultAsync(b => b.ProjectName.Equals(projectName)&&!b.IsDeleted);

    return model;
  }

  public Task<IEnumerable<BuildVersion>> GetAll()
    => Task.FromResult(context.BuildVersions.Where(b=>!b.IsDeleted).AsEnumerable());

  public async Task<BuildVersion?> Delete(int id)
  {
    //TODO Handle soft delete
    BuildVersion? model = await context.BuildVersions.SingleOrDefaultAsync(b => b.Id == id);

    ArgumentNullException.ThrowIfNull(nameof(model));

    //_ = context.Remove(model!);
    model!.IsDeleted=true;

    _ = await context.SaveChangesAsync();

    return model;
  }
}