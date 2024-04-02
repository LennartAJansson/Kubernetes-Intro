namespace BuildVersionsApi.Features.BuildVersions.Increment;

using BuildVersionsApi.Domain.Abstract;
using BuildVersionsApi.Domain.Model;

using FastEndpoints;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

public sealed class IncrementBuildVersionEndpoint(IEndpointsService service)
  : Endpoint<IncrementBuildVersionRequest, IncrementBuildVersionResponse,
    IncrementBuildVersionMapper>
{
  public override void Configure()
  {
    Version(1, deprecateAt: 4);
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

  public override async Task HandleAsync(IncrementBuildVersionRequest request, CancellationToken cancellationToken)
  {
    Logger.LogInformation("Running pipe on Increment");
    string username = User.Identity is not null && User.Identity.Name is not null
      ? User.Identity.Name
      : "John Doe";// string.Empty;

    //HINT Don't like the way we expose the entity to the endpoint, mediator is much better that way
    BuildVersion? entity = await service.HandleIncreaseVersion(request.ProjectName, request.VersionElement, username, cancellationToken);

    if (entity is null)
    {
      await SendNotFoundAsync(cancellation: cancellationToken);
    }
    else
    {
      await SendOkAsync(Map.FromEntity(entity), cancellationToken);
    }
  }
}