namespace BuildVersionsApi.Features.BuildVersions.Create;

using BuildVersionsApi.Features.Domain.Abstract;
using BuildVersionsApi.Features.Domain.Model;

using FastEndpoints;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

public sealed class CreateBuildVersionEndpoint(IPersistanceService service)
  : Endpoint<CreateBuildVersionRequest, CreateBuildVersionResponse,
    CreateBuildVersionMapper>
{
  public override void Configure()
  {
    //Version(1);
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
    BuildVersion? entity = Map.ToEntity(request);
    try
    {
      if (entity is not null)
      {
        entity.Username = User.Identity is not null && User.Identity.Name is not null ? User.Identity.Name : string.Empty;
        entity.Created = DateTime.UtcNow;
        entity.Changed = DateTime.UtcNow;
        entity = await service.CreateProject(entity);
      }
      if (entity is null)
      {
        await SendErrorsAsync(cancellation: cancellationToken);
        return;
      }
      await SendMappedAsync(entity!, ct: cancellationToken);
    }
    catch (Exception)
    {
      throw;
    }
  }
}