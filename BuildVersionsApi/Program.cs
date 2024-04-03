global using FastEndpoints;

using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

using BuildVersionsApi.Domain.Extensions;
using BuildVersionsApi.Domain.Types;

using FastEndpoints.Swagger;

using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.UseSerilog((context, services, configuration) => configuration
  .ReadFrom.Configuration(context.Configuration)
  .ReadFrom.Services(services)
  .Enrich.FromLogContext());

ApplicationInfo appInfo = new(typeof(Program));

builder.Services
  .AddBuildVersionsApiFeatures()
  .AddBuildVersionsApiDomain()
  .AddBuildVersionsApiPersistance(builder.Configuration.GetConnectionString("BuildVersionsDb"));

builder.Services
  .AddFastEndpoints(o =>
  {
    o.Assemblies =
    [
      Assembly.GetAssembly(typeof(FeaturesExtension))!
    ];
  })
  .SwaggerDocument(o =>
  {
    o.MaxEndpointVersion = 1;
    o.DocumentSettings = s =>
    {
      s.DocumentName = "Release 1.0";
      s.Title = "BuildVersions";
      s.Version = "v1.0";
    };
  })
  .SwaggerDocument(o =>
  {
    o.MaxEndpointVersion = 2;
    o.DocumentSettings = s =>
    {
      s.DocumentName = "Release 2.0";
      s.Title = "BuildVersions";
      s.Version = "v2.0";
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

app.ConfigurePersistance();

app.UseCors();

app.UseHttpsRedirection();

app
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
.UseSwaggerGen();

app.Run();