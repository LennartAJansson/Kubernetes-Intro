namespace BuildVersionsApi.Features.BuildVersions.ReadByName;

using BuildVersionsApi.Domain.Abstract;
using BuildVersionsApi.Domain.Model;

using FastEndpoints;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

public sealed class ReadBuildVersionByNameEndpoint
  (ILogger<ReadBuildVersionByNameEndpoint> logger, IDomainService service)
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
      await SendOkAsync(Map.FromEntity(response), cancellationToken);
    }
  }
}