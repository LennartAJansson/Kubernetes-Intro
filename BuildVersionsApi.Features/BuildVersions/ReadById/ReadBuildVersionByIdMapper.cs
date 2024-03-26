namespace BuildVersionsApi.Features.BuildVersions.ReadById;

using AutoMapper;

using BuildVersionsApi.Features.Model;

public class ReadBuildVersionByIdMapper : Profile
{
  public ReadBuildVersionByIdMapper()
  {
    _ = CreateMap<ReadBuildVersionByIdRequest, BuildVersion>();
    _ = CreateMap<BuildVersion, ReadBuildVersionByIdResponse>();
  }
}