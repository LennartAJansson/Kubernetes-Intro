namespace BuildVersionsApi.Domain.Services;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using BuildVersionsApi.Domain.Abstract;
using BuildVersionsApi.Domain.Actions;
using BuildVersionsApi.Domain.Model;
using BuildVersionsApi.Domain.Types;

public class DomainService(IStorageService service) : IDomainService
{
  public async Task<BuildVersion?> HandleCreateProject(BuildVersion buildVersion, CancellationToken cancellationToken)
    => await service.CreateProject(buildVersion, cancellationToken);

  public async Task<BuildVersion?> HandleIncreaseVersion(string projectName, VersionNumber version, CancellationToken cancellationToken)
  {
    BuildVersion? buildVersion = await service.GetByName(projectName, cancellationToken);
    if (buildVersion is null)
    {
      return null;
    }

    buildVersion = buildVersion.IncrementVersion(version);

    return await service.UpdateProject(buildVersion, cancellationToken);
  }

  public async Task<BuildVersion?> HandleUpdateProject(BuildVersion newBuildVersion, CancellationToken cancellationToken)
  {
    BuildVersion? buildVersion = await service.GetById(newBuildVersion.Id, cancellationToken);
    if (buildVersion is null)
    {
      return null;
    }

    buildVersion = buildVersion.CloneValuesFrom(newBuildVersion);

    return await service.UpdateProject(buildVersion, cancellationToken);
  }

  public async Task<BuildVersion?> HandleDelete(string projectName, string username, CancellationToken cancellationToken)
  {
    BuildVersion? buildVersion = await service.GetByName(projectName, cancellationToken);
    if (buildVersion is null)
    {
      return null;
    }

    buildVersion.Username = username;

    return await service.Delete(buildVersion, cancellationToken);
  }

  public async Task<IEnumerable<BuildVersion>> HandleGetAll(CancellationToken cancellationToken) =>
    await service.GetAll(cancellationToken);

  public async Task<BuildVersion?> HandleGetById(int id, CancellationToken cancellationToken) =>
    await service.GetById(id, cancellationToken);

  public async Task<BuildVersion?> HandleGetByName(string projectName, CancellationToken cancellationToken) =>
    await service.GetByName(projectName, cancellationToken);
}