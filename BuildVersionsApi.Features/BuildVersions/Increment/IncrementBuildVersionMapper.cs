namespace BuildVersionsApi.Features.BuildVersions.Increment;

using AutoMapper;

using BuildVersionsApi.Features.Domain.Model;

public class IncrementBuildVersionMapper : Profile
{
  public IncrementBuildVersionMapper() => _ = CreateMap<BuildVersion, IncrementBuildVersionResponse>();
}