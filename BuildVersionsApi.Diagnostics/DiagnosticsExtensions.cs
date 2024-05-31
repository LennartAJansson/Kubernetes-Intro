namespace BuildVersionsApi.Diagnostics;

using BuildVersionsApi.Diagnostics.Checks;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;

using Prometheus;

public static class DiagnosticsExtensions
{
  public static IServiceCollection AddBuildVersionsApiDiagnostics(this IServiceCollection services, IConfiguration configuration,IEnumerable<HealthCheckParam> checks)
  {
    IHealthChecksBuilder builder = services.AddHealthChecks();
    foreach (HealthCheckParam check in checks)
    {
      if (check.Title is not null)
      {
        if (check.Title.StartsWith("http_", StringComparison.CurrentCultureIgnoreCase))
        {
          _ = builder.AddCheck(check.Title ?? string.Empty, new HttpHealthCheck(check));
        }
        else if (check.Title.StartsWith("icmp_", StringComparison.CurrentCultureIgnoreCase))
        {
          _ = builder.AddCheck(check.Title ?? string.Empty, new ICMPHealthCheck(check));
        }
        else if (check.Title.StartsWith("db_", StringComparison.CurrentCultureIgnoreCase))
        {
          check.Host = configuration.GetConnectionString(check.Host ?? string.Empty);
          _ = builder.AddCheck(check.Title ?? string.Empty, new DbHealthCheck(check));
        }
      }
    }
    builder.ForwardToPrometheus();

    _ = services.AddSingleton<ReadAllBuildVersionMetrics>();
    _ = services.AddOpenTelemetry()
        .WithMetrics(builder =>
        {
          _ = builder
            //.AddAspNetCoreInstrumentation()
            .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("BuildVersionsApi"))
            .AddMeter("endpoints-read")
            .AddPrometheusExporter();


          //builder.AddMeter("Microsoft.AspNetCore.Hosting",
          //             "Microsoft.AspNetCore.Server.Kestrel");
          //builder.AddView("http.server.request.duration", new ExplicitBucketHistogramConfiguration
          //{
          //  Boundaries = new double[] { 0, 0.005, 0.01, 0.025, 0.05,
          //             0.075, 0.1, 0.25, 0.5, 0.75, 1, 2.5, 5, 7.5, 10 }
          //});
        });
    return services;
  }
  public static IApplicationBuilder MapBuildVersionsApiDiagnostics(this IApplicationBuilder app)
  {
    _ = ((WebApplication)app).MapHealthChecks("/healthz", new CustomHealthCheckOptions());
    _ = ((WebApplication)app).MapPrometheusScrapingEndpoint();

    return app;
  }
}

