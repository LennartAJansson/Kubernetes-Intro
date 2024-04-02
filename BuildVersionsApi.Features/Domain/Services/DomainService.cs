namespace BuildVersionsApi.Features.Domain.Services;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using BuildVersionsApi.Features.Domain.Abstract;
using BuildVersionsApi.Features.Domain.Model;
using BuildVersionsApi.Features.Types;

public class DomainService(IPersistanceService service) : IDomainService
{
  public async Task<BuildVersion?> HandleCreateProject(BuildVersion buildVersion, string username, CancellationToken cancellationToken)
  {
    //HINT Add business logic here: Should handle the created, changed and username
    buildVersion.Created = DateTime.Now;
    buildVersion.Changed = DateTime.Now;
    buildVersion.Username = username;
    return await service.CreateProject(buildVersion, cancellationToken);
  }

  public async Task<BuildVersion?> HandleDelete(string projectName, string username, CancellationToken cancellationToken)
  {
    //HINT Add business logic here: Using a soft delete on the object and register the changed and username
    BuildVersion? buildVersion = await service.GetByName(projectName, cancellationToken);
    if (buildVersion is null)
    {
      return null;
    }

    buildVersion.Changed = DateTime.Now;
    buildVersion.Username = username;
    buildVersion.IsDeleted = true;

    return await service.Delete(buildVersion, cancellationToken);
  }

  
  public async Task<IEnumerable<BuildVersion>> HandleGetAll(CancellationToken cancellationToken) =>
    //HINT Add business logic here: Returns all that are not soft deleted
    await service.GetAll(cancellationToken);

  public async Task<BuildVersion?> HandleGetById(int id, CancellationToken cancellationToken) =>
    //HINT Add business logic here: Returns single by Id if not soft deleted
    await service.GetById(id, cancellationToken);

  public async Task<BuildVersion?> HandleGetByName(string projectName, CancellationToken cancellationToken) =>
    //HINT Add business logic here: Returns single by ProjectName if not soft deleted
    await service.GetByName(projectName, cancellationToken);

  public async Task<BuildVersion?> HandleIncreaseVersion(string projectName, VersionNumber version, string username, CancellationToken cancellationToken)
  {
    //HINT Add business logic here: Increase Version and register the changed and username
    BuildVersion? buildVersion = await service.GetByName(projectName, cancellationToken);
    if (buildVersion is null)
    {
      return null;
    }

    switch (version)
    {
      case VersionNumber.Major:
        buildVersion.Major++;
        buildVersion.Minor = buildVersion.Build = buildVersion.Revision = 0;
        break;

      case VersionNumber.Minor:
        buildVersion.Minor++;
        buildVersion.Build = buildVersion.Revision = 0;
        break;

      case VersionNumber.Build:
        buildVersion.Build++;
        buildVersion.Revision = 0;
        break;

      case VersionNumber.Revision:
        buildVersion.Revision++;
        break;
    }
    buildVersion.Changed = DateTime.Now;
    buildVersion.Username = username;

    return await service.UpdateProject(buildVersion, cancellationToken);
  }

  public async Task<BuildVersion?> HandleUpdateProject(BuildVersion newBuildVersion, string username, CancellationToken cancellationToken)
  {
    //HINT Add business logic here: Update the object and register the changed and username
    BuildVersion? buildVersion = await service.GetById(newBuildVersion.Id, cancellationToken);
    if (buildVersion is null)
    {
      return null;
    }

    buildVersion.ProjectName = newBuildVersion.ProjectName;
    buildVersion.Major = newBuildVersion.Major;
    buildVersion.Minor = newBuildVersion.Minor;
    buildVersion.Build = newBuildVersion.Build;
    buildVersion.Revision = newBuildVersion.Revision;
    buildVersion.SemanticVersionText = newBuildVersion.SemanticVersionText;
    buildVersion.Changed = DateTime.Now;
    buildVersion.Username = username;

    return await service.UpdateProject(buildVersion, cancellationToken);
  }
}
