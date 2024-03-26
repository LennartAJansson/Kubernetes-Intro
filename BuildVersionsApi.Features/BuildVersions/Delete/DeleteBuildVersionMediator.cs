namespace BuildVersionsApi.Features.BuildVersions.Delete;

using AutoMapper;

using BuildVersionsApi.Features.Model;
using BuildVersionsApi.Features.Persistance.Service;

using MediatR;

public sealed class DeleteBuildVersionMediator(IPersistanceService service, IMapper mapper)
  : IRequestHandler<DeleteBuildVersionRequest, DeleteBuildVersionResponse>
{
  public async Task<DeleteBuildVersionResponse> Handle(DeleteBuildVersionRequest request, CancellationToken cancellationToken)
  {
    BuildVersion? version = await service.Delete(request.Id);

    return mapper.Map<DeleteBuildVersionResponse>(version);
  }
}