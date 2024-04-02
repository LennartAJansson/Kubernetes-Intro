namespace BuildVersionsApi.Features.BuildVersions.ReadByName;

using AutoMapper;

using BuildVersionsApi.Domain.Abstract;
using BuildVersionsApi.Domain.Model;

using MediatR;

public sealed class ReadBuildVersionByNameMediator(IEndpointsService service, IMapper mapper)
  : IRequestHandler<ReadBuildVersionByNameRequest, ReadBuildVersionByNameResponse?>
{
  public async Task<ReadBuildVersionByNameResponse?> Handle(ReadBuildVersionByNameRequest request, CancellationToken cancellationToken)
  {
    BuildVersion? version = await service.HandleGetByName(request.ProjectName, cancellationToken);
    if(version is null)
    {
      return null;
    }

    return mapper.Map<ReadBuildVersionByNameResponse>(version);
  }
}