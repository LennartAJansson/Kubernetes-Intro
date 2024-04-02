namespace BuildVersionsApi.Features.BuildVersions.ReadAll;

using BuildVersionsApi.Features.Domain.Abstract;
using BuildVersionsApi.Features.Domain.Model;

using FastEndpoints;

using MediatR;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

public sealed class ReadAllBuildVersionEndpoint(IDomainService service, ISender sender)
  : EndpointWithoutRequest<IEnumerable<ReadAllBuildVersionResponse>>
{
  public override void Configure()
  {
    Logger.LogInformation("Running configure on ReadAll");
    Version(1, deprecateAt: 4);
    Get("BuildVersion/ReadAll");
    AllowAnonymous();
    Description(b => b
      //.WithGroupName("BuildVersion")
      .WithName("ReadAll")
      .Produces<IEnumerable<ReadAllBuildVersionResponse>>(200, "application/json")
      .ProducesProblemDetails(400, "application/json+problem") //if using RFC errors
      .ProducesProblemFE<InternalErrorResponse>(500)); //if using FE exception handler
    Options(x => x.CacheOutput(p => p.Expire(TimeSpan.FromSeconds(60))));
  }

  public override async Task HandleAsync(CancellationToken cancellationToken)
  {
    Logger.LogInformation("Running pipe on ReadAll");

    //HINT Do not use Response since that will trigger validator
    IEnumerable<ReadAllBuildVersionResponse> response = await sender.Send(new ReadAllBuildVersionRequest(), cancellationToken);

    if (response is null || !response.Any())
    {
      //HINT Should I return empty collection or not found?
      await SendOkAsync([], cancellationToken);
      //await SendNotFoundAsync(cancellationToken);
    }
    else
    {
      await SendOkAsync(response, cancellation: cancellationToken);
    }
  }
}