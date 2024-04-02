namespace BuildVersionsApi.Domain.Types;

using System.Diagnostics;
using System.Reflection;

public sealed class ApplicationInfo(Type type)
{
  public Assembly ExecutingAssembly { get; private set; } = Assembly.GetAssembly(type)!;
  public FileVersionInfo? ExecutingFileVersionInfo => FileVersionInfo.GetVersionInfo(ExecutingAssembly.Location);
  public string? AssemblyVersion => ExecutingAssembly.GetName().Version?.ToString();
  public string? FileVersion => ExecutingFileVersionInfo?.FileVersion;
  public string? SemanticVersion => ExecutingAssembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;
  public string? Description => ExecutingAssembly.GetCustomAttribute<AssemblyDescriptionAttribute>()?.Description;
}