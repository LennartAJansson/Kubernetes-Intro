namespace BuildVersionsApi.Features.BuildVersions.Increment;

using BuildVersionsApi.Domain.Model;
using BuildVersionsApi.Features.BuildVersions.Delete;

using FastEndpoints;

public class IncrementBuildVersionMapper
  : ResponseMapper<IncrementBuildVersionResponse, BuildVersion>
{
  public override IncrementBuildVersionResponse FromEntity(BuildVersion e)
    => new()
    {
      Id = e.Id,
      ProjectName = e.ProjectName,
      Major = e.Major,
      Minor = e.Minor,
      Build = e.Build,
      Revision = e.Revision,
      SemanticVersionText = e.SemanticVersionText,
      Version = e.Version,
      Release = e.Release,
      SemanticVersion = e.SemanticVersion,
    };

  public override Task<IncrementBuildVersionResponse> FromEntityAsync(BuildVersion e, CancellationToken ct = default)
    => Task.FromResult<IncrementBuildVersionResponse>(new()
    {
      Id = e.Id,
      ProjectName = e.ProjectName,
      Major = e.Major,
      Minor = e.Minor,
      Build = e.Build,
      Revision = e.Revision,
      SemanticVersionText = e.SemanticVersionText,
      Version = e.Version,
      Release = e.Release,
      SemanticVersion = e.SemanticVersion,
    });
}