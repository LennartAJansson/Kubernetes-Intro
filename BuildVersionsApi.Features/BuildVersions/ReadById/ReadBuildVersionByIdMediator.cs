namespace BuildVersionsApi.Features.BuildVersions.ReadById;

using AutoMapper;

using BuildVersionsApi.Features.BuildVersions.Create;
using BuildVersionsApi.Features.Model;
using BuildVersionsApi.Features.Persistance.Service;

using MediatR;

public sealed class ReadBuildVersionByIdMediator(IPersistanceService service, IMapper mapper)
  : IRequestHandler<ReadBuildVersionByIdRequest, ReadBuildVersionByIdResponse>
{
  public async Task<ReadBuildVersionByIdResponse> Handle(ReadBuildVersionByIdRequest request, CancellationToken cancellationToken)
  {
    BuildVersion? version = await service.GetById(request.Id);

    return mapper.Map<ReadBuildVersionByIdResponse>(version);
  }
}