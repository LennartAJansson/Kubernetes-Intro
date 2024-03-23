namespace BuildVersionsApi.Features.BuildVersions.ReadAll;

using AutoMapper;

using BuildVersionsApi.Features.BuildVersions.Create;
using BuildVersionsApi.Features.Model;
using BuildVersionsApi.Features.Persistance.Service;

using MediatR;

public sealed class ReadAllBuildVersionMediator(IPersistanceService service, IMapper mapper)
  : IRequestHandler<ReadAllBuildVersionRequest, ReadAllBuildVersionResponse>
{
  public async Task<ReadAllBuildVersionResponse> Handle(ReadAllBuildVersionRequest request, CancellationToken cancellationToken)
  {
    BuildVersion? person = mapper.Map<BuildVersion>(request);
    person = await service.AddProject(person);
    return mapper.Map<ReadAllBuildVersionResponse>(person);
  }
}