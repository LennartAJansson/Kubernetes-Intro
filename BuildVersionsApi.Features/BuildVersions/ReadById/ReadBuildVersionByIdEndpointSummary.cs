namespace BuildVersionsApi.Features.BuildVersions.ReadById;

using FastEndpoints;

public sealed class ReadBuildVersionByIdEndpointSummary
  : Summary<ReadBuildVersionByIdEndpoint>
{
  public ReadBuildVersionByIdEndpointSummary()
  {
    Summary = "Reads a BuildVersion object by its id";
    Description = "Reads a BuildVersion object by its id";
    ExampleRequest = new ReadBuildVersionByIdRequest
    {
      Id = 1
    };
    ResponseExamples[200] = new ReadBuildVersionByIdResponse
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
    Responses[200] = "When Ok it returns a ReadBuildVersionByIdResponse";
    Responses[400] = "When problems occur it returns 400";
    Responses[403] = "When Forbidden it returns 403";
  }
}