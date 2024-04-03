namespace BuildVersionsApi.Domain.Model;

using System.Text.RegularExpressions;

public sealed class BuildVersion : BaseLoggedEntity
{
  //https://devopsnet.com/2011/06/09/build-versioning-strategy/

  public required string ProjectName { get; set; }
  public int Major { get; set; }
  public int Minor { get; set; }
  public int Build { get; set; }
  public int Revision { get; set; }
  //HINT SemanticVersionText = {Major}.{Minor}.{Build}.{Revision} or {Major}.{Minor}.{Build}-{Revision}
  public required string SemanticVersionText { get; set; } = "{Major}.{Minor}.{Build}-dev.{Revision}";

  //Calculated values
  public Version Version => new(Major, Minor, Build, Revision);

  public string Release => $"{Major}.{Minor}";

  public string SemanticVersion => Regex.Replace(SemanticVersionText, @"\{\w+?\}",
    match => GetValue(match.Value));

  private string GetValue(string variable)
  {
    switch (variable.ToLower())
    {
      case "{major}":
        return $"{Major}";
      case "{minor}":
        return $"{Minor}";
      case "{build}":
        return $"{Build}";
      case "{revision}":
        return $"{Revision}";
      default:
        return "";
    }
  }
}