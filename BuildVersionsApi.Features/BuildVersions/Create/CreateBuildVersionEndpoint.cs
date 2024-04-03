namespace BuildVersionsApi.Features.BuildVersions.Create;

using BuildVersionsApi.Domain.Abstract;
using BuildVersionsApi.Domain.Model;

using FastEndpoints;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

public sealed class CreateBuildVersionEndpoint(IEndpointsService service)
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
    Summary(s =>
    {
      //https://fast-endpoints.com/docs/swagger-support#swagger-documentation
      s.Summary = "Create a new BuildVersion";
      s.Description = "Create a new BuildVersion";
      s.ExampleRequest = new CreateBuildVersionRequest
      {
        ProjectName = "NissesTaxi",
        Major = 1,
        Minor = 0,
        Build = 0,
        Revision = 0,
        SemanticVersionText = "{Major}.{Minor}.{Build}.dev.{Revision}"
      };
      s.ResponseExamples[200] = new CreateBuildVersionResponse
      {
        Id = 1,
        ProjectName = "NissesTaxi",
        Major = 1,
        Minor = 0,
        Build = 0,
        Revision = 0,
        SemanticVersionText = "{Major}.{Minor}.{Build}.dev.{Revision}",
        Version = new Version(1, 0, 0, 0),
        Release = "1.0",
        SemanticVersion = "1.0.0.dev.0",
      };
      s.Responses[200] = "When ok returns a CreateBuildVersionResponse";
      s.Responses[403] = "When forbidden it returns 403";
    });
    Options(x => x.CacheOutput(p => p.Expire(TimeSpan.FromSeconds(60))));
  }

  public override async Task HandleAsync(CreateBuildVersionRequest request, CancellationToken cancellationToken)
  {
    Logger.LogInformation("Running pipe on Create");
    string username = User.Identity is not null && User.Identity.Name is not null
      ? User.Identity.Name
      : "John Doe";// string.Empty;

    //HINT Don't like the way we expose the entity to the endpoint, mediator is much better that way
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