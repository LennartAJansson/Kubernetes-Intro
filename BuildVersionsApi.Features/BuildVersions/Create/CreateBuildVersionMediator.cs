namespace BuildVersionsApi.Features.BuildVersions.Create;

using AutoMapper;

using BuildVersionsApi.Features.BuildVersions.Create;
using BuildVersionsApi.Features.Model;
using BuildVersionsApi.Features.Persistance.Service;

using MediatR;

public sealed class CreateBuildVersionMediator(IPersistanceService service, IMapper mapper)
  : IRequestHandler<CreateBuildVersionRequest, CreateBuildVersionResponse>
{
  public async Task<CreateBuildVersionResponse> Handle(CreateBuildVersionRequest request, CancellationToken cancellationToken)
  {
    BuildVersion? version = mapper.Map<BuildVersion>(request);
    version = await service.AddProject(version);

    return mapper.Map<CreateBuildVersionResponse>(version);
  }
}