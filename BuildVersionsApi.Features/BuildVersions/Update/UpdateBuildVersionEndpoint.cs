namespace BuildVersionsApi.Features.BuildVersions.Update;

using BuildVersionsApi.Domain.Abstract;
using BuildVersionsApi.Domain.Model;

using FastEndpoints;

using Microsoft.Extensions.Logging;

public sealed class UpdateBuildVersionEndpoint
  (ILogger<UpdateBuildVersionEndpoint> logger, IDomainService service)
  : Endpoint<UpdateBuildVersionRequest,
    UpdateBuildVersionResponse,
    UpdateBuildVersionMapper>
{
  public override void Configure()
  {
    Version(1, deprecateAt: 4);
    Put("BuildVersion/Update");
    Policies("AdminPolicy");
  }

  public override async Task HandleAsync(UpdateBuildVersionRequest request, CancellationToken cancellationToken)
  {
    logger.LogInformation("Running pipe on Update");

    BuildVersion? entity = Map.ToEntity(request);
    if (entity is not null)
    {
      entity = await service.HandleUpdateProject(entity, cancellationToken);
    }

    if (entity is null)
    {
      await SendNotFoundAsync(cancellationToken);
    }
    else
    {
      await SendOkAsync(Map.FromEntity(entity), cancellation: cancellationToken);
    }
  }
}