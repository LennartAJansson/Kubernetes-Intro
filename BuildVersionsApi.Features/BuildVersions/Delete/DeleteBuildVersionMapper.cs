namespace BuildVersionsApi.Features.BuildVersions.Delete;

using AutoMapper;

using BuildVersionsApi.Features.Model;

public class DeleteBuildVersionMapper : Profile
{
  public DeleteBuildVersionMapper()
  {
    _ = CreateMap<BuildVersion, DeleteBuildVersionResponse>();
  }
}