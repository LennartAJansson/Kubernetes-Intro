namespace BuildVersionsApi.Features.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BuildVersionsApi.Features.Domain.Model;
using BuildVersionsApi.Features.Types;

public interface IDomainService
{
    Task<BuildVersion?> HandleCreateProject(BuildVersion buildVersion);

    Task<BuildVersion?> HandleUpdateProject(BuildVersion buildVersion);

    Task<BuildVersion?> HandleIncreaseVersion(string projectName, VersionNumber version);

    Task<BuildVersion?> HandleGetById(int id);

    Task<BuildVersion?> HandleGetByName(string projectName);

    Task<IEnumerable<BuildVersion>> HandleGetAll();

    Task<BuildVersion?> HandleDelete(int id);
}
