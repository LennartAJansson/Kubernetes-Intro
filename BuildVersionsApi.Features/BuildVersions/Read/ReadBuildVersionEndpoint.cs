namespace BuildVersionsApi.Features.BuildVersions.Read;

using FastEndpoints;

using MediatR;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

public sealed class ReadBuildVersionEndpoint(ISender sender)
  : Endpoint<ReadBuildVersionRequest, ReadBuildVersionResponse>
{
  public override void Configure()
  {
    Version(1);
    Post("BuildVersion/Read");
    AllowAnonymous();
    Description(b => b
      .WithGroupName("BuildVersion")
      .WithName("Read")
      .Accepts<ReadBuildVersionRequest>("application/json")
      .Produces<ReadBuildVersionResponse>(200, "application/json")
      .ProducesProblemDetails(400, "application/json+problem") //if using RFC errors
      .ProducesProblemFE<InternalErrorResponse>(500)); //if using FE exception handler
    Options(x => x.CacheOutput(p => p.Expire(TimeSpan.FromSeconds(60))));
  }

  public override async Task HandleAsync(ReadBuildVersionRequest request, CancellationToken cancellationToken)
  {
    Response = await sender.Send(request, cancellationToken);
  }
}