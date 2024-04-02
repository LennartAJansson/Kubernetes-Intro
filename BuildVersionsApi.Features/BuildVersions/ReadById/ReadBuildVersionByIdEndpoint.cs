namespace BuildVersionsApi.Features.BuildVersions.ReadById;

using BuildVersionsApi.Features.Domain.Abstract;

using FastEndpoints;

using MediatR;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

public sealed class ReadBuildVersionByIdEndpoint(IDomainService service, ISender sender)
  : EndpointWithoutRequest<ReadBuildVersionByIdResponse>
{
  public override void Configure()
  {
    Version(1, deprecateAt: 4);
    Get("BuildVersion/ReadById/{id}");
    AllowAnonymous();
    Description(b => b
      //.WithGroupName("BuildVersion")
      .WithName("ReadById")
      .Produces<ReadBuildVersionByIdResponse>(200, "application/json")
      .ProducesProblemDetails(400, "application/json+problem") //if using RFC errors
      .ProducesProblemFE<InternalErrorResponse>(500)); //if using FE exception handler
    Options(x => x.CacheOutput(p => p.Expire(TimeSpan.FromSeconds(60))));
  }

  public override async Task HandleAsync(CancellationToken cancellationToken)
  {
    Logger.LogInformation("Running pipe on ReadById");
    int id = Route<int>("id");

    //HINT Do not use assignment to Response since that will trigger validator immediately    //HINT Do not use Response since that will trigger validator
    var response = await sender.Send(new ReadBuildVersionByIdRequest { Id = id }, cancellationToken);

    if (response is null)
    {
      await SendNotFoundAsync(cancellationToken);
    }
    else
    {
      await SendOkAsync(response, cancellation: cancellationToken);
    }
  }
}