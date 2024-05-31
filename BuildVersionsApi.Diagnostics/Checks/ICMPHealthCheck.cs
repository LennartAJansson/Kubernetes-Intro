namespace BuildVersionsApi.Diagnostics.Checks;

using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Diagnostics.HealthChecks;

public sealed class ICMPHealthCheck : IHealthCheck
{
  private readonly string? title;
  private readonly string? host;
  private readonly int healthyRoundtripTime;
  private readonly bool active;

  public ICMPHealthCheck(string title, string host, int healthyRoundtripTime, bool active)
  {
    this.title = title;
    this.host = host;
    this.healthyRoundtripTime = healthyRoundtripTime;
    this.active = active;
  }

  public ICMPHealthCheck(HealthCheckParam param)
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
      //context.Registration.Timeout = TimeSpan.MinValue;
      //context.Registration.Period = TimeSpan.MinValue;
      return HealthCheckResult.Healthy("Not active!!!");
    }

    string resolve = string.Empty;
    try
    {
      IPHostEntry iph = Dns.GetHostEntry(host!);
      string ip = string.Empty;
      if (iph != null)
      {
        ip = iph != null && iph.AddressList != null && iph.AddressList.Length > 0
            ? iph.AddressList[0].ToString()
            : string.Empty;
        resolve = $"Resolved {host} to {iph!.HostName} and ipaddress {ip} ";
      }

      using Ping ping = new();
      PingOptions options = new()
      {
        DontFragment = true
      };
      string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
      byte[] buffer = Encoding.ASCII.GetBytes(data);
      int timeout = healthyRoundtripTime;
      PingReply reply = ping.Send(ip, healthyRoundtripTime, buffer, options);
      switch (reply.Status)
      {
        case IPStatus.Success:
          string msg = $"{title} to {host} took {reply.RoundtripTime} ms. {resolve}";
          return reply.RoundtripTime > healthyRoundtripTime
              ? HealthCheckResult.Degraded(msg)
              : HealthCheckResult.Healthy(msg);
        default:
          string err = $"{title} to {host} failed: {reply.Status}. {resolve}";
          return HealthCheckResult.Unhealthy(err);
      }
    }
    catch (Exception ex)
    {
      string err = $"{title} to {host} failed: {resolve}";
      return HealthCheckResult.Unhealthy(err, exception: ex);
    }
  }

  public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
      => Task.FromResult(CheckHealth(context, cancellationToken));
}
