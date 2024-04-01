namespace BuildVersionsApi.Features.BuildVersions.ReadByName;

using BuildVersionsApi.Features.Domain.Abstract;

using FastEndpoints;

using MediatR;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

public sealed class ReadBuildVersionByNameEndpoint(IDomainService service, ISender sender)
  : EndpointWithoutRequest<ReadBuildVersionByNameResponse>
{
  public override void Configure()
  {
    Version(1, deprecateAt: 4);
    Get("BuildVersion/ReadByName/{name}");
    AllowAnonymous();
    Description(b => b
      //.WithGroupName("BuildVersion")
      .WithName("ReadByName")
      .Produces<ReadBuildVersionByNameResponse>(200, "application/json")
      .ProducesProblemDetails(400, "application/json+problem") //if using RFC errors
      .ProducesProblemFE<InternalErrorResponse>(500)); //if using FE exception handler
    Options(x => x.CacheOutput(p => p.Expire(TimeSpan.FromSeconds(60))));
  }

  public override async Task HandleAsync(CancellationToken cancellationToken)
  {
    Logger.LogInformation("Running pipe on ReadByName");
    string? name = Route<string>("name");

    Response = await sender.Send(new ReadBuildVersionByNameRequest { ProjectName = name! }, cancellationToken);

    if (Response is null)
    {
      await SendNotFoundAsync(cancellationToken);
    }
    else
    {
      await SendOkAsync(Response, cancellation: cancellationToken);
    }
  }
}