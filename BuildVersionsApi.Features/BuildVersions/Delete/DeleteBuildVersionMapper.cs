namespace BuildVersionsApi.Features.BuildVersions.Delete;

using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using BuildVersionsApi.Features.BuildVersions.Create;
using BuildVersionsApi.Features.Domain.Model;

using FastEndpoints;

public sealed class DeleteBuildVersionMapper 
  : ResponseMapper<DeleteBuildVersionResponse, BuildVersion>
{
  public override DeleteBuildVersionResponse FromEntity(BuildVersion e) 
    => base.FromEntity(e);
  
  public override Task<DeleteBuildVersionResponse> FromEntityAsync(BuildVersion e, CancellationToken ct = default) 
    => base.FromEntityAsync(e, ct);
}