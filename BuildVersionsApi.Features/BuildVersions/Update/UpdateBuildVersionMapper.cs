namespace BuildVersionsApi.Features.BuildVersions.Update;

using AutoMapper;

using BuildVersionsApi.Features.Model;

public class ReadBuildVersionMapper : Profile
{
  public ReadBuildVersionMapper()
  {
    _ = CreateMap<UpdateBuildVersionRequest, BuildVersion>();
    _ = CreateMap<BuildVersion, UpdateBuildVersionResponse>();
  }
}