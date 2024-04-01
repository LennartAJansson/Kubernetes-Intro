namespace BuildVersionsApi.Features.BuildVersions.Increment;

using BuildVersionsApi.Features.BuildVersions.Delete;
using BuildVersionsApi.Features.Domain.Model;

using FastEndpoints;

public class IncrementBuildVersionMapper
  : ResponseMapper<IncrementBuildVersionResponse, BuildVersion>
{
  public override IncrementBuildVersionResponse FromEntity(BuildVersion e)
    => base.FromEntity(e);

  public override Task<IncrementBuildVersionResponse> FromEntityAsync(BuildVersion e, CancellationToken ct = default)
    => base.FromEntityAsync(e, ct);
}