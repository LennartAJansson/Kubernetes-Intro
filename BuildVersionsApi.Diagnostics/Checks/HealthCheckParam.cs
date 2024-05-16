namespace BuildVersionsApi.Diagnostics.Checks;

public sealed class HealthCheckParam
{
  public string? Title { get; set; }
  public string? Host { get; set; }
  public int HealthyRoundtripTime { get; set; }
  public bool Active { get; set; }
}
