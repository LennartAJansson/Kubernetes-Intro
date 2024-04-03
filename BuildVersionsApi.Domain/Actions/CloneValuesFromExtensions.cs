namespace BuildVersionsApi.Domain.Actions;
using BuildVersionsApi.Domain.Model;

public static class CloneValuesFromExtensions
{
  public static BuildVersion CloneValuesFrom(this BuildVersion target, BuildVersion source)
    => Clone.Create(target).Transfer(source);
}