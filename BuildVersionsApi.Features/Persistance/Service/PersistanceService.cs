namespace BuildVersionsApi.Features.Persistance.Service;

using BuildVersionsApi.Features.Model;
using BuildVersionsApi.Features.Persistance.Context;

using BuildVersionsApi.Features.Types;

using Microsoft.EntityFrameworkCore;

public sealed class PersistanceService(BuildVersionsDbContext context) : IPersistanceService
{
  public async Task<BuildVersion?> AddProject(BuildVersion buildVersion)
  {
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

  public async Task<BuildVersion?> IncreaseVersion(string projectName, VersionNumber version)
  {
    if (!await context.BuildVersions.AnyAsync(b => b.ProjectName == projectName))
    {
      return null;
    }

    BuildVersion model = await context.BuildVersions.SingleAsync(b => b.ProjectName == projectName);

    switch (version)
    {
      case VersionNumber.Major:
        model.Major ++;
        model.Minor = 0;
        model.Build = 0;
        model.Revision = 0;
        break;
      case VersionNumber.Minor:
        model.Minor ++;
        model.Build = 0;
        model.Revision = 0;
        break;
      case VersionNumber.Build:
        model.Build ++;
        model.Revision = 0;
        break;
      case VersionNumber.Revision:
        model.Revision ++;
        break;
    }
    //model.Username = "";

    _ = await context.SaveChangesAsync();

    return model;
  }

  public async Task<BuildVersion?> GetById(int id)
  {
    BuildVersion? model = await context.BuildVersions.SingleOrDefaultAsync(b => b.Id == id);

    return model;
  }

  public async Task<BuildVersion?> GetByName(string projectName)
  {
    BuildVersion? model = await context.BuildVersions.SingleOrDefaultAsync(b => b.ProjectName == projectName);

    return model;
  }

  public Task<IEnumerable<BuildVersion>> GetAll() 
    => Task.FromResult(context.BuildVersions.AsEnumerable());
  
}
