namespace BuildVersionsApi.Features.Model;

public sealed class BuildVersion : BaseLoggedEntity
{
  //https://devopsnet.com/2011/06/09/build-versioning-strategy/

  public required string ProjectName { get; set; }
  public int Major { get; set; }
  public int Minor { get; set; }
  public int Build { get; set; }
  public int Revision { get; set; }
  public string SemanticVersionText { get; set; } = "";

  //Calculated values
  public Version Version => new(Major, Minor, Build, Revision);
  public string Release => $"{Major}.{Minor}";
  public string SemanticVersion => SemanticVersionText == string.Empty
    ? $"{Major}.{Minor}.{Build}"
    : $"{Major}.{Minor}.{Build}-{SemanticVersionText}.{Revision}";
  public string SemanticRelease => SemanticVersionText == string.Empty
    ? $"{Major}.{Minor}"
    : $"{Major}.{Minor}-{SemanticVersionText}.{Build}.{Revision}";
}

