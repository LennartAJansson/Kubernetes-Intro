namespace BuildVersionsApi.Features.BuildVersions.Delete;

using AutoMapper;

using BuildVersionsApi.Features.Model;

public class ReadBuildVersionMapper : Profile
{
  public ReadBuildVersionMapper()
  {
    _ = CreateMap<BuildVersion, DeleteBuildVersionResponse>();
  }
}