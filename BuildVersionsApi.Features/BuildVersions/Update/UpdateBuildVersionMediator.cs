namespace BuildVersionsApi.Features.BuildVersions.Update;

using AutoMapper;
using BuildVersionsApi.Features.Domain.Abstract;
using BuildVersionsApi.Features.Domain.Model;

using MediatR;

public sealed class UpdateBuildVersionMediator(IDomainService service, IMapper mapper)
  : IRequestHandler<UpdateBuildVersionRequest, UpdateBuildVersionResponse>
{
  public async Task<UpdateBuildVersionResponse> Handle(UpdateBuildVersionRequest request, CancellationToken cancellationToken)
  {
    BuildVersion? version = mapper.Map<BuildVersion>(request);
    version = await service.HandleUpdateProject(version);

    return mapper.Map<UpdateBuildVersionResponse>(version);
  }
}