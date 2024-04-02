namespace BuildVersionsApi.Features.Domain.Abstract;
using System.Collections.Generic;
using System.Threading.Tasks;

using BuildVersionsApi.Features.Domain.Model;
using BuildVersionsApi.Features.Types;

public interface IDomainService
{
  Task<BuildVersion?> HandleCreateProject(BuildVersion buildVersion, string username, CancellationToken cancellationToken);
  Task<BuildVersion?> HandleDelete(string projectName, string username, CancellationToken cancellationToken);
  Task<IEnumerable<BuildVersion>> HandleGetAll(CancellationToken cancellationToken);
  Task<BuildVersion?> HandleGetById(int id, CancellationToken cancellationToken);
  Task<BuildVersion?> HandleGetByName(string projectName, CancellationToken cancellationToken);
  Task<BuildVersion?> HandleIncreaseVersion(string projectName, VersionNumber version, string username, CancellationToken cancellationToken);
  Task<BuildVersion?> HandleUpdateProject(BuildVersion buildVersion, string username, CancellationToken cancellationToken);
}
