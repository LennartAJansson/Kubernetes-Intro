namespace BuildVersionsApi.Features.BuildVersions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class BuildVersionResponse
{
  //BuildVersion values:
  public required string ProjectName { get; set; }
  public int Major { get; set; }
  public int Minor { get; set; }
  public int Build { get; set; }
  public int Revision { get; set; }
  public required string SemanticVersionText { get; set; }
  //Calculated values:
  public required Version Version { get; set; }
  public required string Release { get; set; }
  public required string SemanticVersion { get; set; }
  public required string SemanticRelease { get; set; }

  //Base class values:
  public int Id { get; set; }
  public required string Username { get; set; }
  public DateTime Created { get; set; }
  public DateTime Changed { get; set; }
}
