namespace BuildVersionsApi.Features.BuildVersions.Delete;

using BuildVersionsApi.Domain.Model;

using FastEndpoints;

public sealed class DeleteBuildVersionMapper
  : ResponseMapper<DeleteBuildVersionResponse,
    BuildVersion>
{
  public override DeleteBuildVersionResponse FromEntity(BuildVersion e)
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
}