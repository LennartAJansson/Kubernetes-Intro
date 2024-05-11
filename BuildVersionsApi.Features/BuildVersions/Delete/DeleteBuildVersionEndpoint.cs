namespace BuildVersionsApi.Features.BuildVersions.Delete;

using BuildVersionsApi.Domain.Abstract;
using BuildVersionsApi.Domain.Model;

using FastEndpoints;

using Microsoft.Extensions.Logging;

public sealed class DeleteBuildVersionEndpoint
  (ILogger<DeleteBuildVersionEndpoint> logger, IDomainService service)
  : Endpoint<DeleteBuildVersionRequest,
    DeleteBuildVersionResponse,
    DeleteBuildVersionMapper>
{
  public override void Configure()
  {
    Version(2, deprecateAt: 4);
    Delete("BuildVersion/Delete/{projectName}");
    Policies("AdminPolicy");
  }

  public override async Task HandleAsync(DeleteBuildVersionRequest request, CancellationToken cancellationToken)
  {
    logger.LogInformation("Running pipe on Delete");

    BuildVersion? entity = await service.HandleDelete(request.ProjectName, request.Username ?? "John Doe", cancellationToken);

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