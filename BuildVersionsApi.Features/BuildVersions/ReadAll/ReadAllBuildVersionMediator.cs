namespace BuildVersionsApi.Features.BuildVersions.ReadAll;

using AutoMapper;

using BuildVersionsApi.Features.BuildVersions.Create;
using BuildVersionsApi.Features.Model;
using BuildVersionsApi.Features.Persistance.Service;

using MediatR;

public sealed class ReadAllBuildVersionMediator(IPersistanceService service, IMapper mapper)
  : IRequestHandler<ReadAllBuildVersionRequest, IEnumerable<ReadAllBuildVersionResponse>>
{
  public async Task<IEnumerable<ReadAllBuildVersionResponse>> Handle(ReadAllBuildVersionRequest request, CancellationToken cancellationToken)
  {
    IEnumerable<BuildVersion> people = await service.GetAll();

    return mapper.Map<IEnumerable<ReadAllBuildVersionResponse>>(people);
  }
}