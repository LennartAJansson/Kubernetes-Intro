namespace BuildVersionsApi.Features.BuildVersions.ReadByName;

using AutoMapper;

using BuildVersionsApi.Features.BuildVersions.Create;
using BuildVersionsApi.Features.Model;
using BuildVersionsApi.Features.Persistance.Service;

using MediatR;

public sealed class ReadBuildVersionMediator(IPersistanceService service, IMapper mapper)
  : IRequestHandler<ReadBuildVersionByNameRequest, ReadBuildVersionByNameResponse>
{
  public async Task<ReadBuildVersionByNameResponse> Handle(ReadBuildVersionByNameRequest request, CancellationToken cancellationToken)
  {
    BuildVersion? version = await service.GetByName(request.ProjectName);

    return mapper.Map<ReadBuildVersionByNameResponse>(version);
  }
}