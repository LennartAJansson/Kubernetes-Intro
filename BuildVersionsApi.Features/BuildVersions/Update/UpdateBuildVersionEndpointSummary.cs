namespace BuildVersionsApi.Features.BuildVersions.Create;

using BuildVersionsApi.Features.BuildVersions.Update;

using FastEndpoints;

public sealed class UpdateBuildVersionEndpointSummary
  : Summary<UpdateBuildVersionEndpoint>
{
  public UpdateBuildVersionEndpointSummary()
  {
    Summary = "Updates a BuildVersion object";
    Description = "Updates a BuildVersion object for a Project";
    ExampleRequest = new UpdateBuildVersionRequest
    {
      Id = 1,
      ProjectName = "NissesTaxi",
      Major = 1,
      Minor = 0,
      Build = 0,
      Revision = 0,
      SemanticVersionText = "{Major}.{Minor}.{Build}.dev.{Revision}"
    };
    ResponseExamples[200] = new UpdateBuildVersionResponse
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
    Responses[200] = "When Ok it returns a UpdateBuildVersionResponse";
    Responses[400] = "When problems occur it returns 400";
    Responses[403] = "When Forbidden it returns 403";
  }
}