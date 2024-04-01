namespace BuildVersionsApi.Features.BuildVersions.Update;

using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using BuildVersionsApi.Features.Domain.Model;

using FastEndpoints;

public class UpdateBuildVersionMapper 
  : Mapper<UpdateBuildVersionRequest, UpdateBuildVersionResponse, BuildVersion>
{

  public override BuildVersion ToEntity(UpdateBuildVersionRequest r)
    => base.ToEntity(r);

  public override Task<BuildVersion> ToEntityAsync(UpdateBuildVersionRequest r, CancellationToken ct = default) 
    => base.ToEntityAsync(r, ct);

  public override UpdateBuildVersionResponse FromEntity(BuildVersion e) 
    => base.FromEntity(e);

  public override Task<UpdateBuildVersionResponse> FromEntityAsync(BuildVersion e, CancellationToken ct = default) 
    => base.FromEntityAsync(e, ct);
}