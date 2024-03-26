namespace BuildVersionsApi.Features.BuildVersions.Increment;

using AutoMapper;

using BuildVersionsApi.Features.BuildVersions.Create;
using BuildVersionsApi.Features.Model;
using BuildVersionsApi.Features.Persistance.Service;

using MediatR;

public sealed class IncrementBuildVersionMediator(IPersistanceService service, IMapper mapper)
  : IRequestHandler<IncrementBuildVersionRequest, IncrementBuildVersionResponse>
{
  public async Task<IncrementBuildVersionResponse> Handle(IncrementBuildVersionRequest request, CancellationToken cancellationToken)
  {
    BuildVersion? person = mapper.Map<BuildVersion>(request);
    person = await service.UpdateProject(person);

    return mapper.Map<IncrementBuildVersionResponse>(person);
  }
}