namespace BuildVersionsApi.Features.BuildVersions.Increment;

using MediatR;

public class IncrementBuildVersionRequest : IRequest<IncrementBuildVersionResponse>
{
  public int Id { get; set; }
  public required string ProjectName { get; set; }
  public int Major { get; set; }
  public int Minor { get; set; }
  public int Build { get; set; }
  public int Revision { get; set; }
  public required string SemanticVersionText { get; set; }
}