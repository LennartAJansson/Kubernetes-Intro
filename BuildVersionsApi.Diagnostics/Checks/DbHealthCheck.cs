namespace BuildVersionsApi.Diagnostics.Checks;

using System.Diagnostics;
using System.Threading.Tasks;

using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Diagnostics.HealthChecks;

using MySql.Data.MySqlClient;

public sealed class DbHealthCheck : IHealthCheck
{
  private readonly string? title;
  private readonly string? host;
  private readonly int healthyRoundtripTime;
  private readonly bool active;

  public DbHealthCheck(string title, string host, int healthyRoundtripTime, bool active)
  {
    this.title = title;
    this.host = host;
    this.healthyRoundtripTime = healthyRoundtripTime;
    this.active = active;
  }

  public DbHealthCheck(HealthCheckParam param)
  {
    title = param.Title;
    host = param.Host;
    healthyRoundtripTime = param.HealthyRoundtripTime;
    active = param.Active;
  }

  public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
  {
    if (!active)
    {
      //context.Registration.Timeout = TimeSpan.MinValue;
      //context.Registration.Period = TimeSpan.MinValue;
      return HealthCheckResult.Healthy("Not active!!!");
    }
    if (HostIsMsSql(host!))
    {
      return await SqlServerTest(cancellationToken);
    }
    else if (HostIsMySql(host!))
    {
      return await MySqlTest(cancellationToken);
    }

    return HealthCheckResult.Healthy("Not valid test!!!");
  }

  private static bool HostIsMsSql(string host) => host.Contains("Data Source=", StringComparison.OrdinalIgnoreCase) ||
      host.Contains("Data Source=Initial Catalog=", StringComparison.OrdinalIgnoreCase);

  private static bool HostIsMySql(string host) => host.Contains("Server=", StringComparison.OrdinalIgnoreCase);

  private async Task<HealthCheckResult> SqlServerTest(CancellationToken cancellationToken)
  {
    try
    {
      using SqlConnection connection = new(host);
      string db = connection.Database;
      string server = connection.DataSource;
      await connection.OpenAsync(cancellationToken);
      using SqlCommand command = connection.CreateCommand();
      command.CommandText = "SELECT 1";

      Stopwatch stopwatch = Stopwatch.StartNew();
      int result = Convert.ToInt32(await command.ExecuteScalarAsync(cancellationToken));
      stopwatch.Stop();

      string msg = $"Db test against {server}/{db} took {stopwatch.ElapsedMilliseconds} ms. Result: {result}";

      return result != 1
          ? HealthCheckResult.Unhealthy(msg)
          : stopwatch.ElapsedMilliseconds < healthyRoundtripTime
          ? HealthCheckResult.Healthy(msg)
          : HealthCheckResult.Degraded(msg);
    }
    catch (Exception ex)
    {
      string err = $"{title} to {host} failed";
      return HealthCheckResult.Unhealthy(err, exception: ex);
    }
  }
  private async Task<HealthCheckResult> MySqlTest(CancellationToken cancellationToken)
  {
    try
    {
      using MySqlConnection connection = new(host);
      string db = connection.Database;
      string server = connection.DataSource;
      await connection.OpenAsync(cancellationToken);
      using MySqlCommand command = connection.CreateCommand();
      command.CommandText = "SELECT 1";

      Stopwatch stopwatch = Stopwatch.StartNew();
      int result = Convert.ToInt32(await command.ExecuteScalarAsync(cancellationToken));
      stopwatch.Stop();

      string msg = $"Db test against {server}/{db} took {stopwatch.ElapsedMilliseconds} ms. Result: {result}";

      return result != 1
          ? HealthCheckResult.Unhealthy(msg)
          : stopwatch.ElapsedMilliseconds < healthyRoundtripTime
          ? HealthCheckResult.Healthy(msg)
          : HealthCheckResult.Degraded(msg);

    }
    catch (Exception ex)
    {
      string err = $"{title} to {host} failed";
      return HealthCheckResult.Unhealthy(err, exception: ex);
    }
  }

}
