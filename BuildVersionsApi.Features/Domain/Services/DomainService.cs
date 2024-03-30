namespace BuildVersionsApi.Features.Domain.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

using BuildVersionsApi.Features.Domain.Abstract;
using BuildVersionsApi.Features.Domain.Model;
using BuildVersionsApi.Features.Types;

public class DomainService(IPersistanceService service) : IDomainService
{
  public async Task<BuildVersion?> HandleCreateProject(BuildVersion buildVersion)
  {
    //TODO Add business logic here:
    return await service.CreateProject(buildVersion);
  }

  public async Task<BuildVersion?> HandleDelete(int id)
  {
    //TODO Add business logic here:
    return await service.Delete(id);
  }

  public async Task<IEnumerable<BuildVersion>> HandleGetAll()
  {
    //TODO Add business logic here:
    return await service.GetAll();
  }

  public async Task<BuildVersion?> HandleGetById(int id)
  {
    //TODO Add business logic here:
    return await service.GetById(id);
  }

  public async Task<BuildVersion?> HandleGetByName(string projectName)
  {
    //TODO Add business logic here:
    return await service.GetByName(projectName);
  }

  public async Task<BuildVersion?> HandleIncreaseVersion(string projectName, VersionNumber version)
  {
    //TODO Add business logic here:
    BuildVersion? model = await service.GetByName(projectName);
    if (model is null)
    {
      return null;
    }

    switch (version)
    {
      case VersionNumber.Major:
        model.Major++;
        model.Minor = 0;
        model.Build = 0;
        model.Revision = 0;
        break;

      case VersionNumber.Minor:
        model.Minor++;
        model.Build = 0;
        model.Revision = 0;
        break;

      case VersionNumber.Build:
        model.Build++;
        model.Revision = 0;
        break;

      case VersionNumber.Revision:
        model.Revision++;
        break;
    }

    return await service.UpdateProject(model);
  }

  public async Task<BuildVersion?> HandleUpdateProject(BuildVersion buildVersion)
  { 
    //TODO Add business logic here:
    return await service.UpdateProject(buildVersion);
  }
}
