namespace BuildVersionsApi.Features.BuildVersions.Create;

using FastEndpoints;

public sealed class CreateBuildVersionEndpointSummary
  : Summary<CreateBuildVersionEndpoint>
{
  public CreateBuildVersionEndpointSummary()
  {
    Summary = "Create a new BuildVersion object";
    Description = "Create a new BuildVersion object for a Project";
    ExampleRequest = new CreateBuildVersionRequest
    {
      ProjectName = "NissesTaxi",
      Major = 1,
      Minor = 0,
      Build = 0,
      Revision = 0,
      SemanticVersionText = "{Major}.{Minor}.{Build}.dev-{Revision}"
    };
    ResponseExamples[201] = new CreateBuildVersionResponse
    {
      Id = 1,
      ProjectName = "NissesTaxi",
      Major = 1,
      Minor = 0,
      Build = 0,
      Revision = 0,
      SemanticVersionText = "{Major}.{Minor}.{Build}.dev-{Revision}",
      Version = new Version(1, 0, 0, 0),
      Release = "1.0",
      SemanticVersion = "1.0.0.dev-0",
    };
    Responses[201] = "When Ok it returns a CreateBuildVersionResponse and a location-header with the url to the created item";
    Responses[400] = "When problems occur it returns 400";
    Responses[403] = "When Forbidden it returns 403";
  }
}

/*

    Description(b => b
      .ClearDefaultProduces(200)
      .WithName("Create")
      .Accepts<CreateBuildVersionRequest>("application/json")
      .Produces<CreateBuildVersionResponse>(201, "application/json")
      .ProducesProblemDetails(400, "application/json") //if using RFC errors
      .ProducesProblemFE<InternalErrorResponse>(500)); //if using FE exception handler
*/