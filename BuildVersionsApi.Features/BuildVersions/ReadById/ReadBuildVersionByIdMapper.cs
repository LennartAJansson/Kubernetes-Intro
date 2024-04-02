namespace BuildVersionsApi.Features.BuildVersions.ReadById;

using AutoMapper;

using BuildVersionsApi.Domain.Model;

public class ReadBuildVersionByIdMapper : Profile
{
  public ReadBuildVersionByIdMapper()
  {
    _ = CreateMap<ReadBuildVersionByIdRequest, BuildVersion>();
    _ = CreateMap<BuildVersion, ReadBuildVersionByIdResponse>();
  }
}