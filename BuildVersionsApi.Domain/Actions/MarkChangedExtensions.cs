namespace BuildVersionsApi.Domain.Actions;
using BuildVersionsApi.Domain.Model;

public static class MarkChangedExtensions
{
  public static BuildVersion MarkAsChanged(this BuildVersion buildVersion, string username, bool created = false)
    => MarkChanged.Create(buildVersion).MarkAsChanged(username, created);
}
