﻿namespace BuildVersionsApi.Features.BuildVersions.ReadAll;

using FastEndpoints;

using MediatR;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

public sealed class ReadAllBuildVersionEndpoint(ISender sender)
  : EndpointWithoutRequest<IEnumerable<ReadAllBuildVersionResponse>>
{
  public override void Configure()
  {
    //Version(1);
    Get("BuildVersion/ReadAll");
    AllowAnonymous();
    Description(b => b
      .WithGroupName("BuildVersion")
      .WithName("ReadAll")
      .Produces<IEnumerable<ReadAllBuildVersionResponse>>(200, "application/json")
      .ProducesProblemDetails(400, "application/json+problem") //if using RFC errors
      .ProducesProblemFE<InternalErrorResponse>(500)); //if using FE exception handler
    Options(x => x.CacheOutput(p => p.Expire(TimeSpan.FromSeconds(60))));
  }

  public override async Task HandleAsync(CancellationToken cancellationToken)
  {
    Response = await sender.Send(new ReadAllBuildVersionRequest(), cancellationToken);
  }
}