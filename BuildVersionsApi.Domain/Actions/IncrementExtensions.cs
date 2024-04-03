namespace BuildVersionsApi.Domain.Actions;
using BuildVersionsApi.Domain.Model;
using BuildVersionsApi.Domain.Types;

public static class IncrementExtensions
{
  public static BuildVersion IncrementVersion(this BuildVersion buildVersion, VersionNumber number)
    => Increment.Create(buildVersion).IncrementVersion(number);
}