namespace BuildVersionsApi.Features.Persistance.Service;

using BuildVersionsApi.Features.Model;
using BuildVersionsApi.Features.Types;

public interface IPersistanceService
{
  Task<BuildVersion?> AddProject(BuildVersion buildVersion);
  Task<BuildVersion?> UpdateProject(BuildVersion buildVersion);
  Task<BuildVersion?> IncreaseVersion(string projectName, VersionElements version);
  Task<BuildVersion?> GetById(int id);
  Task<BuildVersion?> GetByName(string projectName);
  Task<IEnumerable<BuildVersion>> GetAll();
  Task<BuildVersion?> Delete(int id);
}
