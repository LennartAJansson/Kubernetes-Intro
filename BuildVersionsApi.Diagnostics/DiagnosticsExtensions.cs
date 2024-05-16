namespace BuildVersionsApi.Diagnostics;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;

public static class DiagnosticsExtensions
{
  public static IServiceCollection AddBuildVersionsApiDiagnostics(this IServiceCollection services, IConfiguration configuration)
  {
    _ = services.AddHealthChecks();

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
  public static WebApplication MapBuildVersionsApiDiagnostics(this WebApplication app)
  {
    _ = app.MapHealthChecks("/healthz");
    _ = app.MapPrometheusScrapingEndpoint();

    return app;
  }
}

