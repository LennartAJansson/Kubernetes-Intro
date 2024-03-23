namespace BuildVersionsApi.Features.BuildVersions.Read;

using AutoMapper;

using BuildVersionsApi.Features.BuildVersions.Create;
using BuildVersionsApi.Features.Model;
using BuildVersionsApi.Features.Persistance.Service;

using MediatR;

public sealed class ReadBuildVersionMediator(IPersistanceService service, IMapper mapper)
  : IRequestHandler<ReadBuildVersionRequest, ReadBuildVersionResponse>
{
  public async Task<ReadBuildVersionResponse> Handle(ReadBuildVersionRequest request, CancellationToken cancellationToken)
  {
    BuildVersion? person = mapper.Map<BuildVersion>(request);
    person = await service.AddProject(person);
    return mapper.Map<ReadBuildVersionResponse>(person);
  }
}