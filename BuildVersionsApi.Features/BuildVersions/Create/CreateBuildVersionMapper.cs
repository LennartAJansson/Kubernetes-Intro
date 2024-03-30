namespace BuildVersionsApi.Features.BuildVersions.Create;

using System.Threading;
using System.Threading.Tasks;

using BuildVersionsApi.Features.Domain.Model;

using FastEndpoints;

public sealed class CreateBuildVersionMapper : Mapper<CreateBuildVersionRequest, CreateBuildVersionResponse, BuildVersion>
{
  public override Task<BuildVersion> ToEntityAsync(CreateBuildVersionRequest r, CancellationToken ct = default)
    => Task.FromResult<BuildVersion>(new()
    {
      ProjectName = r.ProjectName,
      Major = r.Major,
      Minor = r.Minor,
      Build = r.Build,
      Revision = r.Revision,
      SemanticVersionText = r.SemanticVersionText
    });

  public override BuildVersion ToEntity(CreateBuildVersionRequest r)
    => new()
    {
      ProjectName = r.ProjectName,
      Major = r.Major,
      Minor = r.Minor,
      Build = r.Build,
      Revision = r.Revision,
      SemanticVersionText = r.SemanticVersionText
    };

  public override Task<CreateBuildVersionResponse> FromEntityAsync(BuildVersion e, CancellationToken ct = default)
    => Task.FromResult<CreateBuildVersionResponse>(new()
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
      SemanticRelease = e.SemanticRelease,
      Username = e.Username,
      Created = e.Created,
      Changed = e.Changed
    });

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
      SemanticVersion = e.SemanticVersion,
      SemanticRelease = e.SemanticRelease,
      Username = e.Username,
      Created = e.Created,
      Changed = e.Changed
    };
}