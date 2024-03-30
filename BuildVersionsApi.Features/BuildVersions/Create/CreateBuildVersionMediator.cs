namespace BuildVersionsApi.Features.BuildVersions.Create;

using AutoMapper;
using BuildVersionsApi.Features.Domain.Abstract;
using BuildVersionsApi.Features.Domain.Model;

using MediatR;

public sealed class CreateBuildVersionMediator(IDomainService service, IMapper mapper)
  : IRequestHandler<CreateBuildVersionRequest, CreateBuildVersionResponse>
{
  public async Task<CreateBuildVersionResponse> Handle(CreateBuildVersionRequest request, CancellationToken cancellationToken)
  {
    BuildVersion? version = mapper.Map<BuildVersion>(request);
    version = await service.HandleCreateProject(version);

    return mapper.Map<CreateBuildVersionResponse>(version);
  }
}