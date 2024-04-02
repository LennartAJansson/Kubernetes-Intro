namespace BuildVersionsApi.Features.BuildVersions.Update;

using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using BuildVersionsApi.Features.BuildVersions.Delete;
using BuildVersionsApi.Features.Domain.Model;

using FastEndpoints;

public class UpdateBuildVersionMapper 
  : Mapper<UpdateBuildVersionRequest, UpdateBuildVersionResponse, BuildVersion>
{

  public override BuildVersion ToEntity(UpdateBuildVersionRequest r)
    => new()
    {
      Id = r.Id,
      ProjectName = r.ProjectName,
      Major = r.Major,
      Minor = r.Minor,
      Build = r.Build,
      Revision = r.Revision,
      SemanticVersionText = r.SemanticVersionText
    };

  public override Task<BuildVersion> ToEntityAsync(UpdateBuildVersionRequest r, CancellationToken ct = default) 
    => Task.FromResult<BuildVersion>(new()
    {
      Id = r.Id,
      ProjectName = r.ProjectName,
      Major = r.Major,
      Minor = r.Minor,
      Build = r.Build,
      Revision = r.Revision,
      SemanticVersionText = r.SemanticVersionText
    });

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

  public override Task<UpdateBuildVersionResponse> FromEntityAsync(BuildVersion e, CancellationToken ct = default) 
    => Task.FromResult<UpdateBuildVersionResponse>(new()
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