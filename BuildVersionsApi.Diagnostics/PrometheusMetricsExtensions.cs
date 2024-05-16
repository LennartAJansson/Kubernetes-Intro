namespace BuildVersionsApi.Diagnostics;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

using Prometheus;

public static class PrometheusMetricsExtensions
{
    public static IApplicationBuilder UsePrometheusMetrics(this IApplicationBuilder app)
    {
        app = app.UseHttpMetrics();

        return app;
    }
    public static IEndpointRouteBuilder MapMetricsEndpoint(this IEndpointRouteBuilder routes)
    {
        _ = routes.MapMetrics("/metrics");

        return routes;
    }

    public static IEndpointRouteBuilder MapPing(this IEndpointRouteBuilder routes)
    {
        _ = routes.Map("/ping", () => "pong")
        .WithDisplayName("Ping");

        return routes;
    }

}

