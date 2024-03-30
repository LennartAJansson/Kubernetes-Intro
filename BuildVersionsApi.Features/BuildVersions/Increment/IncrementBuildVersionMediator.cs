namespace BuildVersionsApi.Features.BuildVersions.Increment;

using AutoMapper;
using BuildVersionsApi.Features.Domain.Abstract;
using MediatR;

public sealed class IncrementBuildVersionMediator(IDomainService service, IMapper mapper)
  : IRequestHandler<IncrementBuildVersionRequest, IncrementBuildVersionResponse>
{
  public async Task<IncrementBuildVersionResponse> Handle(IncrementBuildVersionRequest request, CancellationToken cancellationToken)
  {
    Domain.Model.BuildVersion? version = await service.HandleIncreaseVersion(request.ProjectName, request.VersionElement);

    return mapper.Map<IncrementBuildVersionResponse>(version);
  }
}