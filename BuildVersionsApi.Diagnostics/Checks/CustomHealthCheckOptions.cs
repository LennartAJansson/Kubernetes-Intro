namespace BuildVersionsApi.Diagnostics.Checks;

using System.Net.Mime;
using System.Text.Json;

using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;

public sealed class CustomHealthCheckOptions : HealthCheckOptions
{
  public CustomHealthCheckOptions()
      : base()
  {
    JsonSerializerOptions jsonSerializerOptions = new()
    {
      WriteIndented = true
    };

    ResponseWriter = async (c, r) =>
    {
      c.Response.ContentType = MediaTypeNames.Application.Json;
      c.Response.StatusCode = StatusCodes.Status200OK;
      string result = JsonSerializer.Serialize(new
      {
        checks = r.Entries
        .Where(e => e.Value.Description != "Not active!!!")
        .Select(e =>
        {
          return new
          {
            name = e.Key,
            responseTime = e.Value.Duration.TotalMilliseconds,
            status = e.Value.Status.ToString(),
            description = e.Value.Description,
          };
        }),
        totalResponseTime = r.TotalDuration.TotalMilliseconds,
      }, jsonSerializerOptions);
      await c.Response.WriteAsync(result);
    };
  }
}