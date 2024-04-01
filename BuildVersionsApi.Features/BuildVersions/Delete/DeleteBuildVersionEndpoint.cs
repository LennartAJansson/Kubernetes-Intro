namespace BuildVersionsApi.Features.BuildVersions.Delete;

using BuildVersionsApi.Features.BuildVersions.Create;
using BuildVersionsApi.Features.Domain.Abstract;

using FastEndpoints;

using MediatR;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

public sealed class DeleteBuildVersionEndpoint(IDomainService service)
  : EndpointWithoutRequest<DeleteBuildVersionResponse,
    DeleteBuildVersionMapper>
{
  public override void Configure()
  {
    Version(2, deprecateAt: 4);
    Delete("BuildVersion/Delete/{id}");
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
    int id = Route<int>("id");
    var username = User.Identity is not null && User.Identity.Name is not null
      ? User.Identity.Name
      : string.Empty;

    var entity = await service.HandleDelete(id, username, cancellationToken);

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