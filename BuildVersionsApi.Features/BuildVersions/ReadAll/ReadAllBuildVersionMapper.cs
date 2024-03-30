namespace BuildVersionsApi.Features.BuildVersions.ReadAll;

using AutoMapper;

using BuildVersionsApi.Features.Domain.Model;

public class ReadAllBuildVersionMapper : Profile
{
  public ReadAllBuildVersionMapper() =>
    //TODO Mapping for IEnumerable<BuildVersion> to IEnumerable<ReadAllBuildVersionResponse>
    _ = CreateMap<BuildVersion, ReadAllBuildVersionResponse>();
}