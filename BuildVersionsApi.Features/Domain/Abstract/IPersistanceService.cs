namespace BuildVersionsApi.Features.Domain.Abstract;

using BuildVersionsApi.Features.Domain.Model;
using BuildVersionsApi.Features.Types;

public interface IPersistanceService
{
    Task<BuildVersion?> CreateProject(BuildVersion buildVersion);

    Task<BuildVersion?> UpdateProject(BuildVersion buildVersion);

    Task<BuildVersion?> GetById(int id);

    Task<BuildVersion?> GetByName(string projectName);

    Task<IEnumerable<BuildVersion>> GetAll();

    Task<BuildVersion?> Delete(int id);
}