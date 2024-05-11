namespace BuildVersionsApi.Features.BuildVersions.ReadByName;

using BuildVersionsApi.Domain.Model;

using FastEndpoints;

public sealed class ReadBuildVersionByNameMapper
  : ResponseMapper<ReadBuildVersionByNameResponse,
    BuildVersion>
{
  public override ReadBuildVersionByNameResponse FromEntity(BuildVersion e) => new()
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
    SemanticVersion = e.SemanticVersion
  };
}