namespace BuildVersionsApi.Features.BuildVersions.Increment;

using BuildVersionsApi.Domain.Types;

using FastEndpoints;

public sealed class IncrementBuildVersionEndpointSummary
  : Summary<IncrementBuildVersionEndpoint>
{
  public IncrementBuildVersionEndpointSummary()
  {
    Summary = "Increment the VersionNumber of a BuildVersion object";
    Description = "Increment the VersionNumber of a BuildVersion object for a Project. A VersionNumber is either Major, Minor, Build or Revision";
    ExampleRequest = new IncrementBuildVersionRequest
    {
      ProjectName = "NissesTaxi",
      VersionElement = VersionNumber.Major,
    };
    ResponseExamples[200] = new IncrementBuildVersionResponse
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
    Responses[200] = "When Ok it returns a IncrementBuildVersionResponse";
    Responses[400] = "When problems occur it returns 400";
    Responses[403] = "When Forbidden it returns 403";
  }
}