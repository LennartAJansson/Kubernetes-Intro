namespace BuildVersionsApi.Features.Domain.Services;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using BuildVersionsApi.Features.Domain.Abstract;
using BuildVersionsApi.Features.Domain.Model;
using BuildVersionsApi.Features.Types;

public class DomainService(IPersistanceService service) : IDomainService
{
  public async Task<BuildVersion?> HandleCreateProject(BuildVersion buildVersion, CancellationToken cancellationToken) =>
    //HINT Add business logic here: Should handle the created, changed and username
    await service.CreateProject(buildVersion, cancellationToken);

  public async Task<BuildVersion?> HandleDelete(string projectName, string username, CancellationToken cancellationToken)
  {
    //HINT Add business logic here: Using a soft delete on the object and register the changed and username
    BuildVersion? model = await service.GetByName(projectName, cancellationToken);
    if (model is null)
    {
      return null;
    }

    model.Changed = DateTime.Now;
    model.Username = username;
    model.IsDeleted = true;

    return await service.Delete(model, cancellationToken);
  }

  public Task<BuildVersion?> HandleDelete(int id, string username, CancellationToken cancellationToken) => throw new NotImplementedException();
  
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
    BuildVersion? model = await service.GetByName(projectName, cancellationToken);
    if (model is null)
    {
      return null;
    }

    switch (version)
    {
      case VersionNumber.Major:
        model.Major++;
        model.Minor = model.Build = model.Revision = 0;
        break;

      case VersionNumber.Minor:
        model.Minor++;
        model.Build = model.Revision = 0;
        break;

      case VersionNumber.Build:
        model.Build++;
        model.Revision = 0;
        break;

      case VersionNumber.Revision:
        model.Revision++;
        break;
    }
    model.Changed = DateTime.Now;
    model.Username = username;

    return await service.UpdateProject(model, cancellationToken);
  }

  public async Task<BuildVersion?> HandleUpdateProject(BuildVersion buildVersion, CancellationToken cancellationToken)
  {
    //HINT Add business logic here: Update the object and register the changed and username
    BuildVersion? model = await service.GetByName(buildVersion.ProjectName, cancellationToken);
    if (model is null)
    {
      return null;
    }
    model.ProjectName = buildVersion.ProjectName;
    model.Major = buildVersion.Major;
    model.Minor = buildVersion.Minor;
    model.Build = buildVersion.Build;
    model.Revision = buildVersion.Revision;
    model.SemanticVersionText = buildVersion.SemanticVersionText;
    model.Changed = DateTime.Now;
    model.Username = buildVersion.Username;

    return await service.UpdateProject(model, cancellationToken);
  }
}
