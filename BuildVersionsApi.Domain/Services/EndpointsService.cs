namespace BuildVersionsApi.Domain.Services;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using BuildVersionsApi.Domain.Abstract;
using BuildVersionsApi.Domain.Actions;
using BuildVersionsApi.Domain.Model;
using BuildVersionsApi.Domain.Types;

public class EndpointsService(IStorageService service) : IEndpointsService
{
  public async Task<BuildVersion?> HandleCreateProject(BuildVersion buildVersion, string username, CancellationToken cancellationToken)
  {
    buildVersion = buildVersion.MarkAsChanged(username, true);

    return await service.CreateProject(buildVersion, cancellationToken);
  }

  public async Task<BuildVersion?> HandleIncreaseVersion(string projectName, VersionNumber version, string username, CancellationToken cancellationToken)
  {
    BuildVersion? buildVersion = await service.GetByName(projectName, cancellationToken);
    if (buildVersion is null)
    {
      return null;
    }

    buildVersion = buildVersion.IncrementVersion(version).MarkAsChanged(username);

    return await service.UpdateProject(buildVersion, cancellationToken);
  }

  public async Task<BuildVersion?> HandleUpdateProject(BuildVersion newBuildVersion, string username, CancellationToken cancellationToken)
  {
    BuildVersion? buildVersion = await service.GetById(newBuildVersion.Id, cancellationToken);
    if (buildVersion is null)
    {
      return null;
    }

    buildVersion = buildVersion.CloneValuesFrom(newBuildVersion).MarkAsChanged(username);

    return await service.UpdateProject(buildVersion, cancellationToken);
  }

  public async Task<BuildVersion?> HandleDelete(string projectName, string username, CancellationToken cancellationToken)
  {
    BuildVersion? buildVersion = await service.GetByName(projectName, cancellationToken);
    if (buildVersion is null)
    {
      return null;
    }

    buildVersion = buildVersion.MarkAsChanged(username);

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
}
