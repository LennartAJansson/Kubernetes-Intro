namespace BuildVersionsApi.Features.BuildVersions.Update;

using FastEndpoints;

using MediatR;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

public sealed class UpdateBuildVersionEndpoint(ISender sender)
  : Endpoint<UpdateBuildVersionRequest, UpdateBuildVersionResponse>
{
  public override void Configure()
  {
    //Version(1);
    Put("BuildVersion/Update");
    AllowAnonymous();
    Description(b => b
      //.WithGroupName("BuildVersion")
      .WithName("Update")
      .Accepts<UpdateBuildVersionRequest>("application/json")
      .Produces<UpdateBuildVersionResponse>(200, "application/json")
      .ProducesProblemDetails(400, "application/json+problem") //if using RFC errors
      .ProducesProblemFE<InternalErrorResponse>(500)); //if using FE exception handler
    Options(x => x.CacheOutput(p => p.Expire(TimeSpan.FromSeconds(60))));
  }

  public override async Task HandleAsync(UpdateBuildVersionRequest request, CancellationToken cancellationToken) => Response = await sender.Send(request, cancellationToken);
}