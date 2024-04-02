namespace BuildVersionsApi.Features.BuildVersions.Increment;

using BuildVersionsApi.Domain.Types;

using MediatR;

public class IncrementBuildVersionRequest : IRequest<IncrementBuildVersionResponse>
{
  public required string ProjectName { get; set; }
  public VersionNumber VersionElement { get; set; }
}