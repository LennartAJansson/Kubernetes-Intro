namespace BuildVersionsApi.Diagnostics.Checks;

using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;

using Microsoft.Extensions.Diagnostics.HealthChecks;

public sealed class HttpHealthCheck : IHealthCheck
{
  private readonly string? title;
  private readonly string? host;
  private readonly int healthyRoundtripTime;
  private readonly bool active;
  public HttpHealthCheck(string title, string host, int healthyRoundtripTime, bool active)
  {
    this.title = title;
    this.host = host;
    this.healthyRoundtripTime = healthyRoundtripTime;
    this.active = active;
  }
  public HttpHealthCheck(HealthCheckParam param)
  {
    title = param.Title;
    host = param.Host;
    healthyRoundtripTime = param.HealthyRoundtripTime;
    active = param.Active;
  }
  public HealthCheckResult CheckHealth(HealthCheckContext context, CancellationToken cancellationToken = default)
  {
    if (!active)
    {
      return HealthCheckResult.Healthy("Not active!!!");
    }

    try
    {
      using HttpClient client = new();
      client.BaseAddress = new Uri(host!);
      client.Timeout = TimeSpan.FromMilliseconds(healthyRoundtripTime * 2);
      Stopwatch stopwatch = Stopwatch.StartNew();
      using HttpResponseMessage response = client.GetAsync("/", cancellationToken).Result;
      long elapsed = stopwatch.ElapsedMilliseconds;
      stopwatch.Stop();
      string msg = $"{title} to {host} took {elapsed} ms.";

      return response.StatusCode == HttpStatusCode.OK
          ? elapsed <= healthyRoundtripTime ? HealthCheckResult.Healthy(msg) : HealthCheckResult.Degraded(msg)
          : HealthCheckResult.Unhealthy(msg);
    }
    catch (Exception e)
    {
      string err = $"{title} to {host} failed: {e.Message}.";
      return HealthCheckResult.Unhealthy(err);
    }
  }
  public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default) => Task.FromResult(CheckHealth(context, cancellationToken));
}
