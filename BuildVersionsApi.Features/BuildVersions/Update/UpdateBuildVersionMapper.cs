namespace BuildVersionsApi.Features.BuildVersions.Update;

using BuildVersionsApi.Domain.Model;

using FastEndpoints;

public sealed class UpdateBuildVersionMapper
  : Mapper<UpdateBuildVersionRequest,
    UpdateBuildVersionResponse,
    BuildVersion>
{
  public override BuildVersion ToEntity(UpdateBuildVersionRequest r)
    => new()
    {
      Id = r.Id,
      Username = r.Username,
      ProjectName = r.ProjectName,
      Major = r.Major,
      Minor = r.Minor,
      Build = r.Build,
      Revision = r.Revision,
      SemanticVersionText = r.SemanticVersionText
    };

  public override UpdateBuildVersionResponse FromEntity(BuildVersion e)
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