namespace BuildVersionsApi.Features.BuildVersions.Update;

using MediatR;

public class UpdateBuildVersionRequest : IRequest<UpdateBuildVersionResponse>
{
  public required string ProjectName { get; set; }
  public int Major { get; set; }
  public int Minor { get; set; }
  public int Build { get; set; }
  public int Revision { get; set; }
  public required string SemanticVersionText { get; set; }
}