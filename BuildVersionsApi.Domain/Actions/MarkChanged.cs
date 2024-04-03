namespace BuildVersionsApi.Domain.Actions;
using System;

using BuildVersionsApi.Domain.Model;

public class MarkChanged
{
  private readonly BuildVersion buildVersion;

  private MarkChanged(BuildVersion buildVersion) => this.buildVersion = buildVersion;

  public static MarkChanged Create(BuildVersion buildVersion)
    => new(buildVersion);

  public BuildVersion MarkAsChanged(string username, bool created = false)
  {
    if (created)
    {
      buildVersion.Created = DateTime.Now;
    }

    buildVersion.Changed = DateTime.Now;
    buildVersion.Username = username;
   
    return buildVersion;
  }
}
