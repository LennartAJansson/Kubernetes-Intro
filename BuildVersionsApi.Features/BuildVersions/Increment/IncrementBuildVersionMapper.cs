namespace BuildVersionsApi.Features.BuildVersions.Increment;

using AutoMapper;

using BuildVersionsApi.Features.Model;

public class IncrementBuildVersionMapper : Profile
{
  public IncrementBuildVersionMapper()
  {
    _ = CreateMap<IncrementBuildVersionRequest, BuildVersion>();
    _ = CreateMap<BuildVersion, IncrementBuildVersionResponse>();
  }
}