namespace BuildVersionsApi.Features.BuildVersions.ReadByName;

using FastEndpoints;

public sealed class ReadBuildVersionByNameEndpointSummary
  : Summary<ReadBuildVersionByNameEndpoint>
{
  public ReadBuildVersionByNameEndpointSummary()
  {
    Summary = "Reads a BuildVersion object by its name";
    Description = "Reads a BuildVersion object by its name";
    ExampleRequest = new ReadBuildVersionByNameRequest
    {
      ProjectName = "NissesTaxi",
    };
    ResponseExamples[200] = new ReadBuildVersionByNameResponse
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
    Responses[200] = "When Ok it returns a ReadBuildVersionByNameResponse";
    Responses[400] = "When problems occur it returns 400";
    Responses[403] = "When Forbidden it returns 403";
  }
}