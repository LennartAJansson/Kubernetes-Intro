namespace BuildVersionsApi.Features.BuildVersions.ReadAll;

using AutoMapper;

using BuildVersionsApi.Features.Model;

public class ReadAllBuildVersionMapper : Profile
{
  public ReadAllBuildVersionMapper()
  {
    _ = CreateMap<ReadAllBuildVersionRequest, BuildVersion>();
    _ = CreateMap<BuildVersion, ReadAllBuildVersionResponse>();
  }
}