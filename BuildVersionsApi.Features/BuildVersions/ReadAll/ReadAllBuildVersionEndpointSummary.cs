namespace BuildVersionsApi.Features.BuildVersions.ReadAll;

using FastEndpoints;

public sealed class ReadAllBuildVersionEndpointSummary
  : Summary<ReadAllBuildVersionEndpoint>
{
  public ReadAllBuildVersionEndpointSummary()
  {
    Summary = "Reads all BuildVersion objects";
    Description = "Reads all BuildVersion objects";
    ResponseExamples[200] = new[] {new ReadAllBuildVersionResponse
    {
      Id = 1,
      ProjectName = "NissesTaxi",
      Major = 1,
      Minor = 0,
      Build = 0,
      Revision = 0,
      SemanticVersionText = "{Major}.{Minor}.{Build}.dev-Revision}",
      Version = new Version(1, 0, 0, 0),
      Release = "1.0",
      SemanticVersion = "1.0.0.dev-0",
    }};
    Responses[200] = "When Ok it returns an array of ReadAllBuildVersionResponse";
    Responses[400] = "When problems occur it returns 400";
    Responses[403] = "When Forbidden it returns 403";
  }
}