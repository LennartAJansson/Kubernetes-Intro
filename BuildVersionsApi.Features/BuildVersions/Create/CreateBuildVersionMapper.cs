namespace BuildVersionsApi.Features.BuildVersions.Create;

using AutoMapper;

using BuildVersionsApi.Features.Model;

public class CreateBuildVersionMapper : Profile
{
  public CreateBuildVersionMapper()
  {
    _ = CreateMap<CreateBuildVersionRequest, BuildVersion>();
    _ = CreateMap<BuildVersion, CreateBuildVersionResponse>();
  }
}