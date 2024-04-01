namespace BuildVersionsApi.Features.BuildVersions.ReadAll;

using AutoMapper;

using BuildVersionsApi.Features.Domain.Model;

public class ReadAllBuildVersionMapper : Profile
{
  //TODO Mapping for IEnumerable<BuildVersion> to IEnumerable<ReadAllBuildVersionResponse>
  public ReadAllBuildVersionMapper() =>
    _ = CreateMap<BuildVersion, ReadAllBuildVersionResponse>();
}