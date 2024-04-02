namespace BuildVersionsApi.Features.BuildVersions.Delete;

using BuildVersionsApi.Features.Domain.Abstract;
using BuildVersionsApi.Features.Domain.Model;

using FastEndpoints;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

public sealed class DeleteBuildVersionEndpoint(IDomainService service)
  : EndpointWithoutRequest<DeleteBuildVersionResponse,
    DeleteBuildVersionMapper>
{
  public override void Configure()
  {
    Version(2, deprecateAt: 4);
    Delete("BuildVersion/Delete/{name}");
    AllowAnonymous();
    Description(b => b
      //.WithGroupName("BuildVersion")
      .WithName("Delete")
      .Produces<DeleteBuildVersionResponse>(200, "application/json")
      .ProducesProblemDetails(400, "application/json+problem") //if using RFC errors
      .ProducesProblemFE<InternalErrorResponse>(500)); //if using FE exception handler
    Options(x => x.CacheOutput(p => p.Expire(TimeSpan.FromSeconds(60))));
  }

  public override async Task HandleAsync(CancellationToken cancellationToken)
  {
    Logger.LogInformation("Running pipe on Delete");
    string? name = Route<string>("name");
    string username = User.Identity is not null && User.Identity.Name is not null
      ? User.Identity.Name
      : "John Doe";// string.Empty;

    BuildVersion? entity = await service.HandleDelete(name!, username, cancellationToken);

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