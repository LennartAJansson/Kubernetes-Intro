namespace BuildVersionsApi.Features.BuildVersions.Update;

using AutoMapper;

using BuildVersionsApi.Features.Domain.Model;

public class ReadBuildVersionMapper : Profile
{
  public ReadBuildVersionMapper()
  {
    _ = CreateMap<UpdateBuildVersionRequest, BuildVersion>();
    _ = CreateMap<BuildVersion, UpdateBuildVersionResponse>();
  }
}