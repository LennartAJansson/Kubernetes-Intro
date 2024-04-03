namespace BuildVersionsApi.Features.Ping;
using System.Threading.Tasks;

using BuildVersionsApi.Features.BuildVersions.ReadById;

using FastEndpoints;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

public sealed class PingEndpoint
  : EndpointWithoutRequest<string>
{
  public override void Configure()
  {
    Version(1);
    Get("Ping");
    AllowAnonymous();
    Description(b => b
      .WithName("Ping")
      .Produces<string>(200, "application/text"));
  }

  public override async Task HandleAsync(CancellationToken cancellationToken)
  {
      await SendOkAsync("pong", cancellation: cancellationToken);
  }
}