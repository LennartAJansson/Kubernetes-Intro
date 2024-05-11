namespace BuildVersionsApi.Features.BuildVersions.Increment;

using BuildVersionsApi.Domain.Types;

public sealed class IncrementBuildVersionRequest
{
  public string ProjectName { get; set; }
  public VersionNumber VersionElement { get; set; }
}