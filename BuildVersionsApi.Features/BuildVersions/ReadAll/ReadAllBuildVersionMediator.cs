namespace BuildVersionsApi.Features.BuildVersions.ReadAll;

using AutoMapper;

using BuildVersionsApi.Domain.Abstract;
using BuildVersionsApi.Domain.Model;

using MediatR;

public sealed class ReadAllBuildVersionMediator(IEndpointsService service, IMapper mapper)
  : IRequestHandler<ReadAllBuildVersionRequest, IEnumerable<ReadAllBuildVersionResponse>>
{
  public async Task<IEnumerable<ReadAllBuildVersionResponse>> Handle(ReadAllBuildVersionRequest request, CancellationToken cancellationToken)
  {
    IEnumerable<BuildVersion> people = await service.HandleGetAll(cancellationToken);

    return mapper.Map<IEnumerable<ReadAllBuildVersionResponse>>(people);
  }
}