namespace BuildVersionsApi.Features.BuildVersions.Increment;

using FastEndpoints;

using MediatR;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

public sealed class IncrementBuildVersionEndpoint(ISender sender)
  : Endpoint<IncrementBuildVersionRequest, IncrementBuildVersionResponse>
{
  public override void Configure()
  {
    //Version(1);
    Put("BuildVersion/Increment");
    AllowAnonymous();
    Description(b => b
      //.WithGroupName("BuildVersion")
      .WithName("Increment")
      .Accepts<IncrementBuildVersionRequest>("application/json")
      .Produces<IncrementBuildVersionResponse>(200, "application/json")
      .ProducesProblemDetails(400, "application/json+problem") //if using RFC errors
      .ProducesProblemFE<InternalErrorResponse>(500)); //if using FE exception handler
    Options(x => x.CacheOutput(p => p.Expire(TimeSpan.FromSeconds(60))));
  }

  public override async Task HandleAsync(IncrementBuildVersionRequest request, CancellationToken cancellationToken) => Response = await sender.Send(request, cancellationToken);
}