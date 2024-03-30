namespace BuildVersionsApi.Features.BuildVersions.ReadById;

using AutoMapper;
using BuildVersionsApi.Features.Domain.Abstract;
using BuildVersionsApi.Features.Domain.Model;

using MediatR;

public sealed class ReadBuildVersionByIdMediator(IDomainService service, IMapper mapper)
  : IRequestHandler<ReadBuildVersionByIdRequest, ReadBuildVersionByIdResponse>
{
  public async Task<ReadBuildVersionByIdResponse> Handle(ReadBuildVersionByIdRequest request, CancellationToken cancellationToken)
  {
    BuildVersion? version = await service.HandleGetById(request.Id);

    return mapper.Map<ReadBuildVersionByIdResponse>(version);
  }
}