namespace BuildVersionsApi.Features.BuildVersions.ReadAll;

using BuildVersionsApi.Domain.Abstract;

using FastEndpoints;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

public sealed class ReadAllBuildVersionEndpoint
  (ILogger<ReadAllBuildVersionEndpoint> logger, IDomainService service)
  : EndpointWithoutRequest<IEnumerable<ReadAllBuildVersionResponse>,
    ReadAllBuildVersionMapper>
{
  public override void Configure()
  {
    Logger.LogInformation("Running configure on ReadAll");
    Version(1, deprecateAt: 4);
    Get("BuildVersion/ReadAll");
    AllowAnonymous();
    Options(x => x.CacheOutput(p => p.Expire(TimeSpan.FromSeconds(60))));
  }

  public override async Task HandleAsync(CancellationToken cancellationToken)
  {
    logger.LogInformation("Running pipe on ReadAll");
    IEnumerable<Domain.Model.BuildVersion>? result = await service.HandleGetAll(cancellationToken);

    if (result is null)
    {
      await SendNotFoundAsync(cancellationToken);
    }
    else if (!result.Any())
    {
      await SendOkAsync([], cancellationToken);
    }
    else
    {
      IEnumerable<ReadAllBuildVersionResponse> response = result.Select(Map.FromEntity);
      await SendOkAsync(response, cancellation: cancellationToken);
    }
  }
}