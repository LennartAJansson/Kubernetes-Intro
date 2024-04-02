namespace BuildVersionsApi.Features.BuildVersions.ReadById;

using AutoMapper;

using BuildVersionsApi.Domain.Abstract;
using BuildVersionsApi.Domain.Model;

using MediatR;

public sealed class ReadBuildVersionByIdMediator(IEndpointsService service, IMapper mapper)
  : IRequestHandler<ReadBuildVersionByIdRequest, ReadBuildVersionByIdResponse>
{
  public async Task<ReadBuildVersionByIdResponse> Handle(ReadBuildVersionByIdRequest request, CancellationToken cancellationToken)
  {
    BuildVersion? version = await service.HandleGetById(request.Id, cancellationToken);

    return mapper.Map<ReadBuildVersionByIdResponse>(version);
  }
}