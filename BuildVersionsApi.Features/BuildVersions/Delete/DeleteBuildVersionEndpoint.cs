namespace BuildVersionsApi.Features.BuildVersions.Delete;

using FastEndpoints;

using MediatR;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

public sealed class DeleteBuildVersionEndpoint(ISender sender)
  : EndpointWithoutRequest<DeleteBuildVersionResponse>
{
  public override void Configure()
  {
    //Version(1);
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
    Response = await sender.Send(new DeleteBuildVersionRequest { Id = id }, cancellationToken);
  }
}