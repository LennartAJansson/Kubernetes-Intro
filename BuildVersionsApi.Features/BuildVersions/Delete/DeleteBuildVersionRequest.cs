namespace BuildVersionsApi.Features.BuildVersions.Delete;

using System.Security.Claims;

using FastEndpoints;

public sealed class DeleteBuildVersionRequest
{
  [FromClaim(claimType: ClaimTypes.Email)]
  public string? Username { get; set; }

  public string ProjectName { get; set; }
}