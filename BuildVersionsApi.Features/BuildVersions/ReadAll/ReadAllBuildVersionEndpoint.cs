namespace BuildVersionsApi.Features.BuildVersions.ReadAll;

using FastEndpoints;

using MediatR;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

public sealed class ReadAllBuildVersionEndpoint(ISender sender)
  : Endpoint<ReadAllBuildVersionRequest, ReadAllBuildVersionResponse>
{
  public override void Configure()
  {
    Version(1);
    Post("BuildVersion/ReadAll");
    AllowAnonymous();
    Description(b => b
      .WithGroupName("BuildVersion")
      .WithName("ReadAll")
      .Accepts<ReadAllBuildVersionRequest>("application/json")
      .Produces<ReadAllBuildVersionResponse>(200, "application/json")
      .ProducesProblemDetails(400, "application/json+problem") //if using RFC errors
      .ProducesProblemFE<InternalErrorResponse>(500)); //if using FE exception handler
    Options(x => x.CacheOutput(p => p.Expire(TimeSpan.FromSeconds(60))));
  }

  public override async Task HandleAsync(ReadAllBuildVersionRequest request, CancellationToken cancellationToken)
  {
    Response = await sender.Send(request, cancellationToken);
  }
}