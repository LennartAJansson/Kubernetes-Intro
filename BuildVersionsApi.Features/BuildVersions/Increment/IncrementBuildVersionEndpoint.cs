namespace BuildVersionsApi.Features.BuildVersions.Increment;

using BuildVersionsApi.Domain.Abstract;
using BuildVersionsApi.Domain.Model;

using FastEndpoints;

using Microsoft.Extensions.Logging;

public sealed class IncrementBuildVersionEndpoint
  (ILogger<IncrementBuildVersionEndpoint> logger, IDomainService service)
  : Endpoint<IncrementBuildVersionRequest,
    IncrementBuildVersionResponse,
    IncrementBuildVersionMapper>
{
  public override void Configure()
  {
    Version(1, deprecateAt: 4);
    Put("BuildVersion/Increment");
    AllowAnonymous();
  }

  public override async Task HandleAsync(IncrementBuildVersionRequest request, CancellationToken cancellationToken)
  {
    logger.LogInformation("Running pipe on Increment");

    BuildVersion? entity = await service.HandleIncreaseVersion(request.ProjectName, request.VersionElement, cancellationToken);

    if (entity is null)
    {
      await SendNotFoundAsync(cancellation: cancellationToken);
    }
    else
    {
      await SendOkAsync(Map.FromEntity(entity), cancellationToken);
    }
  }
}