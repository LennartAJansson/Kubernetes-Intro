namespace BuildVersionsApi.Domain.Actions;
using BuildVersionsApi.Domain.Model;
using BuildVersionsApi.Domain.Types;

internal class Increment
{
  private readonly BuildVersion buildVersion;

  private Increment(BuildVersion buildVersion) => this.buildVersion = buildVersion;

  public static Increment Create(BuildVersion buildVersion)
    => new(buildVersion);

  public BuildVersion IncrementVersion(VersionNumber number)
  {
    switch (number)
    {
      case VersionNumber.Major:
        buildVersion.Major++;
        buildVersion.Minor = buildVersion.Build = buildVersion.Revision = 0;
        break;

      case VersionNumber.Minor:
        buildVersion.Minor++;
        buildVersion.Build = buildVersion.Revision = 0;
        break;

      case VersionNumber.Build:
        buildVersion.Build++;
        buildVersion.Revision = 0;
        break;

      case VersionNumber.Revision:
        buildVersion.Revision++;
        break;
    }

    return buildVersion;
  }
}
