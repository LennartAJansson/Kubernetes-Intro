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
    var version = await service.IncreaseVersion(request.ProjectName,request.VersionElement);

    return mapper.Map<IncrementBuildVersionResponse>(version);
  }
}