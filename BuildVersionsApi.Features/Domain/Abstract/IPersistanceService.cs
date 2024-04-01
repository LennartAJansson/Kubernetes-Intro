namespace BuildVersionsApi.Features.Domain.Abstract;

using BuildVersionsApi.Features.Domain.Model;

public interface IPersistanceService
{
  Task<BuildVersion?> CreateProject(BuildVersion buildVersion, CancellationToken cancellationToken);

  Task<BuildVersion?> UpdateProject(BuildVersion buildVersion, CancellationToken cancellationToken);

  Task<BuildVersion?> GetById(int id, CancellationToken cancellationToken);

  Task<BuildVersion?> GetByName(string projectName, CancellationToken cancellationToken);

  Task<IEnumerable<BuildVersion>> GetAll(CancellationToken cancellationToken);

  Task<BuildVersion?> Delete(BuildVersion buildVersion, CancellationToken cancellationToken);
}