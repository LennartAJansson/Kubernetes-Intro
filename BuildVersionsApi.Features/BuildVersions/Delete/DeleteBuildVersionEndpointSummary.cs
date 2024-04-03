namespace BuildVersionsApi.Features.BuildVersions.Delete;

using FastEndpoints;

public sealed class DeleteBuildVersionEndpointSummary : Summary<DeleteBuildVersionEndpoint>
{
  public DeleteBuildVersionEndpointSummary()
  {
    Summary = "Soft delete a BuildVersion object";
    Description = "Soft delete a BuildVersion object for a Project";
    ResponseExamples[200] = new DeleteBuildVersionResponse
    {
      Id = 1,
      ProjectName = "NissesTaxi",
      Major = 1,
      Minor = 0,
      Build = 0,
      Revision = 0,
      SemanticVersionText = "{Major}.{Minor}.{Build}.dev.{Revision}",
      Version = new Version(1, 0, 0, 0),
      Release = "1.0",
      SemanticVersion = "1.0.0.dev.0",
    };
    Responses[200] = "When Ok it has performed a soft delete and returns a DeleteBuildVersionResponse";
    Responses[403] = "When Forbidden it returns 403";
  }
}
