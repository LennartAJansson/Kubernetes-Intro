namespace BuildVersionsApi.Features.BuildVersions.Delete;

using AutoMapper;
using BuildVersionsApi.Features.Domain.Abstract;
using BuildVersionsApi.Features.Domain.Model;

using MediatR;

public sealed class DeleteBuildVersionMediator(IDomainService service, IMapper mapper)
  : IRequestHandler<DeleteBuildVersionRequest, DeleteBuildVersionResponse>
{
  public async Task<DeleteBuildVersionResponse> Handle(DeleteBuildVersionRequest request, CancellationToken cancellationToken)
  {
    BuildVersion? version = await service.HandleDelete(request.Id);

    return mapper.Map<DeleteBuildVersionResponse>(version);
  }
}