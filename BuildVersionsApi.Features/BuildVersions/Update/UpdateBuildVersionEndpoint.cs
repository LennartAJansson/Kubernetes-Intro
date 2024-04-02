namespace BuildVersionsApi.Features.BuildVersions.Update;

using BuildVersionsApi.Features.Domain.Abstract;
using BuildVersionsApi.Features.Domain.Model;

using FastEndpoints;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

public sealed class UpdateBuildVersionEndpoint(IDomainService service)
  : Endpoint<UpdateBuildVersionRequest, UpdateBuildVersionResponse, UpdateBuildVersionMapper>
{
  public override void Configure()
  {
    Version(1, deprecateAt: 4);
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

  public override async Task HandleAsync(UpdateBuildVersionRequest request, CancellationToken cancellationToken)
  {
    Logger.LogInformation("Running pipe on Update");
    string username = User.Identity is not null && User.Identity.Name is not null
      ? User.Identity.Name
      : "Nisse";// string.Empty;

    BuildVersion? entity = Map.ToEntity(request);
    if (entity is not null)
    {
      entity = await service.HandleUpdateProject(entity, username, cancellationToken);
    }

    if (entity is null)
    {
      await SendNotFoundAsync(cancellationToken);
    }
    else
    {
      await SendOkAsync(Map.FromEntity(entity), cancellation: cancellationToken);
    }

  }
}