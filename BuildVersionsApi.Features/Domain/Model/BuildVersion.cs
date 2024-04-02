namespace BuildVersionsApi.Features.Domain.Model;

using System.Text.RegularExpressions;

public sealed class BuildVersion : BaseLoggedEntity
{
  //https://devopsnet.com/2011/06/09/build-versioning-strategy/

  public required string ProjectName { get; set; }
  public int Major { get; set; }
  public int Minor { get; set; }
  public int Build { get; set; }
  public int Revision { get; set; }
  //TODO: Make a user defined formatting string for all four numbers
  //SemanticVersionText = {Major}.{Minor}.{Build}.{Revision} or {Major}.{Minor}.{Build}-{Revision}
  //Use only SemanticVersion and remove SemanticRelease

  //public required string SemanticVersionText { get; set; } = "";
  ///<example>{Major}.{Minor}.{Build}-dev.{Revision}</example>
  public required string SemanticVersionText { get; set; } = "{Major}.{Minor}.{Build}-dev.{Revision}";

  //Calculated values
  public Version Version => new(Major, Minor, Build, Revision);

  public string Release => $"{Major}.{Minor}";

  //public string SemanticVersion => SemanticVersionText == string.Empty
  //  ? $"{Major}.{Minor}.{Build}"
  //  : $"{Major}.{Minor}.{Build}-{SemanticVersionText}.{Revision}";

  //public string SemanticRelease => SemanticVersionText == string.Empty
  //  ? $"{Major}.{Minor}"
  //  : $"{Major}.{Minor}-{SemanticVersionText}.{Build}.{Revision}";
  public string SemanticVersion =>
    Regex.Replace(SemanticVersionText, @"\{\w+?\}",
    match => GetValue(match.Value));

  private string GetValue(string variable)
  {
    switch (variable)
    {
      case "{major}":
      case "{Major}":
        return $"{Major}";
      case "{minor}":
      case "{Minor}":
        return $"{Minor}";
      case "{build}":
      case "{Build}":
        return $"{Build}";
      case "{revision}":
      case "{Revision}":
        return $"{Revision}";
      default:
        return "";
    }
  }
}