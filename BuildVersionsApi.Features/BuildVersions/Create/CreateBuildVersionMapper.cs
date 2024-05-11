namespace BuildVersionsApi.Features.BuildVersions.Create;

using BuildVersionsApi.Domain.Model;

using FastEndpoints;

public sealed class CreateBuildVersionMapper
  : Mapper<CreateBuildVersionRequest,
    CreateBuildVersionResponse,
    BuildVersion>
{
  public override BuildVersion ToEntity(CreateBuildVersionRequest r)
    => new()
    {
      Username = r.Username,
      ProjectName = r.ProjectName,
      Major = r.Major,
      Minor = r.Minor,
      Build = r.Build,
      Revision = r.Revision,
      SemanticVersionText = r.SemanticVersionText
    };

  public override CreateBuildVersionResponse FromEntity(BuildVersion e)
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
      SemanticVersion = e.SemanticVersion
    };
}