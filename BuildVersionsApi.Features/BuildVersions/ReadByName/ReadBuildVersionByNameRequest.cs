namespace BuildVersionsApi.Features.BuildVersions.ReadByName;

using MediatR;

public class ReadBuildVersionByNameRequest : IRequest<ReadBuildVersionByNameResponse?>
{
  public required string ProjectName { get; set; }
}