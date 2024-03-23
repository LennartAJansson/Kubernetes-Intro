namespace BuildVersionsApi.Features.BuildVersions.Create;

using FastEndpoints;

using MediatR;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

public sealed class CreateBuildVersionEndpoint(ISender sender)
  : Endpoint<CreateBuildVersionRequest, CreateBuildVersionResponse>
{
  public override void Configure()
  {
    Version(1);
    Post("BuildVersion/Create");
    AllowAnonymous();
    Description(b => b
      .WithGroupName("BuildVersion")
      .WithName("Create")
      .Accepts<CreateBuildVersionRequest>("application/json")
      .Produces<CreateBuildVersionResponse>(200, "application/json")
      .ProducesProblemDetails(400, "application/json+problem") //if using RFC errors
      .ProducesProblemFE<InternalErrorResponse>(500)); //if using FE exception handler
    Options(x => x.CacheOutput(p => p.Expire(TimeSpan.FromSeconds(60))));
  }

  public override async Task HandleAsync(CreateBuildVersionRequest request, CancellationToken cancellationToken)
  {
    Response = await sender.Send(request, cancellationToken);
  }
}