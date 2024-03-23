namespace BuildVersionsApi.Features.BuildVersions.Read;

using AutoMapper;

using BuildVersionsApi.Features.Model;

public class ReadBuildVersionMapper : Profile
{
  public ReadBuildVersionMapper()
  {
    _ = CreateMap<ReadBuildVersionRequest, BuildVersion>();
    _ = CreateMap<BuildVersion, ReadBuildVersionResponse>();
  }
}