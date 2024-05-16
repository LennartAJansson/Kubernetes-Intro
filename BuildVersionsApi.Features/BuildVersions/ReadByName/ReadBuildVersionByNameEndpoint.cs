namespace BuildVersionsApi.Features.BuildVersions.ReadByName;

using BuildVersionsApi.Diagnostics;
using BuildVersionsApi.Domain.Abstract;
using BuildVersionsApi.Domain.Model;

using FastEndpoints;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

public sealed class ReadBuildVersionByNameEndpoint
  (ILogger<ReadBuildVersionByNameEndpoint> logger, IDomainService service, ReadAllBuildVersionMetrics metrics)
  : Endpoint<ReadBuildVersionByNameRequest,
    ReadBuildVersionByNameResponse,
    ReadBuildVersionByNameMapper>
{
  public override void Configure()
  {
    Version(1, deprecateAt: 4);
    Get("BuildVersion/ReadByName/{projectName}");
    AllowAnonymous();
    Options(x => x.CacheOutput(p => p.Expire(TimeSpan.FromSeconds(60))));
  }

  public override async Task HandleAsync(ReadBuildVersionByNameRequest request, CancellationToken cancellationToken)
  {
    logger.LogInformation("Running pipe on ReadByName");

    BuildVersion? response = await service.HandleGetByName(request.ProjectName, cancellationToken);
    if (response is null)
    {
      await SendNotFoundAsync(cancellationToken);
    }
    else
    {
      metrics.CountReadById("ReadByName", 1);
      await SendOkAsync(Map.FromEntity(response), cancellationToken);
    }
  }
}