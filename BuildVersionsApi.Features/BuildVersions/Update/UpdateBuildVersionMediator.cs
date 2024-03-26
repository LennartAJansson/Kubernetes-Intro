namespace BuildVersionsApi.Features.BuildVersions.Update;

using AutoMapper;

using BuildVersionsApi.Features.BuildVersions.Create;
using BuildVersionsApi.Features.Model;
using BuildVersionsApi.Features.Persistance.Service;

using MediatR;

public sealed class UpdateBuildVersionMediator(IPersistanceService service, IMapper mapper)
  : IRequestHandler<UpdateBuildVersionRequest, UpdateBuildVersionResponse>
{
  public async Task<UpdateBuildVersionResponse> Handle(UpdateBuildVersionRequest request, CancellationToken cancellationToken)
  {
    BuildVersion? version = mapper.Map<BuildVersion>(request);
    version = await service.UpdateProject(version);

    return mapper.Map<UpdateBuildVersionResponse>(version);
  }
}