namespace BuildVersionsApi.Features.BuildVersions.ReadById;

using BuildVersionsApi.Diagnostics;
using BuildVersionsApi.Domain.Abstract;

using FastEndpoints;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

public sealed class ReadBuildVersionByIdEndpoint
  (ILogger<ReadBuildVersionByIdEndpoint> logger, IDomainService service, ReadAllBuildVersionMetrics metrics)
  : Endpoint<ReadBuildVersionByIdRequest,
    ReadBuildVersionByIdResponse,
    ReadBuildVersionByIdMapper>
{
  public override void Configure()
  {
    Version(1, deprecateAt: 4);
    Get("BuildVersion/ReadById/{id}");
    AllowAnonymous();
    Options(x => x.CacheOutput(p => p.Expire(TimeSpan.FromSeconds(60))));
  }

  public override async Task HandleAsync(ReadBuildVersionByIdRequest request, CancellationToken cancellationToken)
  {
    logger.LogInformation("Running pipe on ReadById");

    Domain.Model.BuildVersion? result = await service.HandleGetById(request.Id, cancellationToken);
    if (result is null)
    {
      await SendNotFoundAsync(cancellationToken);
    }
    else
    {
      metrics.CountReadById("ReadById", 1);
      await SendOkAsync(Map.FromEntity(result), cancellationToken);
    }
  }
}