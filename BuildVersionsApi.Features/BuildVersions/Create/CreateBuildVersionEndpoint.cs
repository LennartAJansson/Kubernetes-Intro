namespace BuildVersionsApi.Features.BuildVersions.Create;

using BuildVersionsApi.Features.Domain.Abstract;
using BuildVersionsApi.Features.Domain.Model;

using FastEndpoints;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

public sealed class CreateBuildVersionEndpoint(IDomainService service)
  : Endpoint<CreateBuildVersionRequest, CreateBuildVersionResponse,
    CreateBuildVersionMapper>
{
  public override void Configure()
  {
    Version(1, deprecateAt: 4);
    Post("BuildVersion/Create");
    AllowAnonymous();
    Description(b => b
      //.WithGroupName("BuildVersion")
      .WithName("Create")
      .Accepts<CreateBuildVersionRequest>("application/json")
      .Produces<CreateBuildVersionResponse>(200, "application/json")
      .ProducesProblemDetails(400, "application/json") //if using RFC errors
      .ProducesProblemFE<InternalErrorResponse>(500)); //if using FE exception handler
    Options(x => x.CacheOutput(p => p.Expire(TimeSpan.FromSeconds(60))));
  }

  public override async Task HandleAsync(CreateBuildVersionRequest request, CancellationToken cancellationToken)
  {
    Logger.LogInformation("Running pipe on Create");
    string username = User.Identity is not null && User.Identity.Name is not null
      ? User.Identity.Name
      : "John Doe";// string.Empty;

    BuildVersion? entity = Map.ToEntity(request);
    if (entity is not null)
    {
      entity = await service.HandleCreateProject(entity, username, cancellationToken);
    }

    if (entity is null)
    {
      await SendErrorsAsync(cancellation: cancellationToken);
    }
    else
    {
      await SendCreatedAtAsync("ReadById", new { id = entity.Id }, Map.FromEntity(entity), true, cancellationToken);
    }
  }
}