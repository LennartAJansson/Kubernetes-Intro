namespace BuildVersionsApi.Domain.Actions;

using BuildVersionsApi.Domain.Model;

public class Clone
{
  private readonly BuildVersion target;

  private Clone(BuildVersion target) => this.target = target;

  public static Clone Create(BuildVersion target)
    => new(target);

  public BuildVersion Transfer(BuildVersion source)
  {
    target.ProjectName = source.ProjectName;
    target.Major = source.Major;
    target.Minor = source.Minor;
    target.Build = source.Build;
    target.Revision = source.Revision;
    target.SemanticVersionText = source.SemanticVersionText;
    target.Username = source.Username;
    target.Changed = source.Changed;
    target.Created = source.Created;

    return target;
  }
}