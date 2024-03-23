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
    BuildVersion? person = mapper.Map<BuildVersion>(request);
    person = await service.AddProject(person);
    return mapper.Map<UpdateBuildVersionResponse>(person);
  }
}