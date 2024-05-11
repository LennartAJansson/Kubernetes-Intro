namespace BuildVersionsApi.Features.BuildVersions.Create;

using BuildVersionsApi.Domain.Abstract;
using BuildVersionsApi.Domain.Model;

using FastEndpoints;

using Microsoft.Extensions.Logging;

public sealed class CreateBuildVersionEndpoint
  (ILogger<CreateBuildVersionEndpoint> logger, IDomainService service)
  : Endpoint<CreateBuildVersionRequest,
    CreateBuildVersionResponse,
    CreateBuildVersionMapper>
{
  public override void Configure()
  {
    Version(1);
    Post("BuildVersion/Create");
    Policies("AdminPolicy");
  }

  public override async Task HandleAsync(CreateBuildVersionRequest request, CancellationToken cancellationToken)
  {
    logger.LogInformation("Running pipe on Create");

    BuildVersion? entity = Map.ToEntity(request);
    if (entity is not null)
    {
      entity = await service.HandleCreateProject(entity, cancellationToken);
    }

    if (entity is null)
    {
      await SendErrorsAsync(cancellation: cancellationToken);
    }
    else
    {
      await SendCreatedAtAsync("ReadById", new { id = entity.Id }, Map.FromEntity(entity), true, cancellationToken);
    }
  }
}