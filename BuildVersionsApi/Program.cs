global using FastEndpoints;

using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

using Auth.Module;

using BuildVersionsApi.Domain.Extensions;
using BuildVersionsApi.Domain.Types;
using BuildVersionsApi.Diagnostics;

using FastEndpoints.Swagger;

using Serilog;
using BuildVersionsApi.Diagnostics.Checks;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, services, configuration) => configuration
  .ReadFrom.Configuration(context.Configuration)
  .ReadFrom.Services(services)
  .Enrich.FromLogContext());

ApplicationInfo appInfo = new(typeof(Program));
builder.Services.AddSingleton(appInfo);

builder.Services.ConfigureHttpJsonOptions(options => {
  options.SerializerOptions.PropertyNameCaseInsensitive = true;
  options.SerializerOptions.WriteIndented = true;
  options.SerializerOptions.AllowTrailingCommas = true;
  options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
  options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

IEnumerable<HealthCheckParam>? healthChecks = builder.Configuration.GetSection("HealthChecks").Get<IEnumerable<HealthCheckParam>>()
    ?? [];

builder.Services
  .AddAuthModule(builder.Configuration)
  .AddBuildVersionsApiFeatures()
  .AddBuildVersionsApiDomain()
  .AddBuildVersionsApiPersistance(builder.Configuration.GetConnectionString("BuildVersionsDb"))
  .AddBuildVersionsApiDiagnostics(builder.Configuration, healthChecks);

builder.Services
  .AddFastEndpoints(o =>
  {
    o.Assemblies =
    [
      Assembly.GetAssembly(typeof(FeaturesExtension))!,
      Assembly.GetAssembly(typeof(AuthModuleExtension))!
    ];
  })
  .SwaggerDocument(o =>
  {
    o.MaxEndpointVersion = 1;
    o.DocumentSettings = s =>
    {
      s.DocumentName = "Release 1.0";
      s.Title = "BuildVersionsApi";
      s.Version = appInfo.SemanticVersion;// "v1.0";
      s.Description = appInfo.Description;
    };
  })
  .SwaggerDocument(o =>
  {
    o.MaxEndpointVersion = 2;
    o.DocumentSettings = s =>
    {
      s.DocumentName = "Release 2.0";
      s.Title = "BuildVersions";
      s.Version = appInfo.SemanticVersion;// "v1.0";
      s.Description = appInfo.Description;
    };
  })
  .AddResponseCaching();

builder.Services.AddCors(options =>
{
  options.AddDefaultPolicy(policy =>
  {
    _ = policy.AllowAnyOrigin()
      .AllowAnyHeader()
      .AllowAnyMethod();
  });
});

WebApplication app = builder.Build();

app.UpdateIdentityDb();
app.ConfigurePersistance();

app.UseCors();

//app.UseHttpsRedirection();

app
  .UseApiAuth()
  .UseResponseCaching()
  .UseDefaultExceptionHandler()
  .UseFastEndpoints(c =>
  {
    c.Versioning.Prefix = "v";
    c.Endpoints.RoutePrefix = "api";
    c.Serializer.Options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    c.Serializer.Options.PropertyNameCaseInsensitive = true;
    c.Serializer.Options.Converters.Add(new JsonStringEnumConverter());
  })
  .UseSwaggerGen()
  .MapBuildVersionsApiDiagnostics();

app.Run();