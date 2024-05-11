namespace BuildVersionsApi.Features.BuildVersions;

using System;

public class BuildVersionResponse
{
  //Base class values:
  public int Id { get; set; }

  //public string? Username { get; set; }
  //public DateTime? Created { get; set; }
  //public DateTime? Changed { get; set; }
  //public bool IsDeleted { get; set; } = false;

  //BuildVersion values:
  public string ProjectName { get; set; }

  public int Major { get; set; }
  public int Minor { get; set; }
  public int Build { get; set; }
  public int Revision { get; set; }
  public string SemanticVersionText { get; set; }

  //Calculated values:
  public Version Version { get; set; }

  public string Release { get; set; }
  public string SemanticVersion { get; set; }
}