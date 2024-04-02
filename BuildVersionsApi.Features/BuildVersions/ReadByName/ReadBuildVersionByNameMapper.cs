namespace BuildVersionsApi.Features.BuildVersions.ReadByName;

using AutoMapper;

using BuildVersionsApi.Domain.Model;

public class ReadBuildVersionByNameMapper : Profile
{
  public ReadBuildVersionByNameMapper()
  {
    _ = CreateMap<ReadBuildVersionByNameRequest, BuildVersion>();
    _ = CreateMap<BuildVersion, ReadBuildVersionByNameResponse>();
  }
}