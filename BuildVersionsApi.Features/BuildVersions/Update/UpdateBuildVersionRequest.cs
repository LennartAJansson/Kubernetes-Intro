namespace BuildVersionsApi.Features.BuildVersions.Update;

using System.Security.Claims;

using FastEndpoints;

public sealed class UpdateBuildVersionRequest
{
  [FromClaim(claimType: ClaimTypes.Email)]
  public string? Username { get; set; }

  public int Id { get; set; }
  public string ProjectName { get; set; }
  public int Major { get; set; }
  public int Minor { get; set; }
  public int Build { get; set; }
  public int Revision { get; set; }
  public string SemanticVersionText { get; set; }
}