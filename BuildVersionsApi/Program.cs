global using FastEndpoints;

using System.Reflection;
using System.Text.Json;

using BuildVersionsApi.Features;
using BuildVersionsApi.Features.Types;

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
  .AddBuildVersionsFeatures(builder.Configuration.GetConnectionString("BuildVersionsDb"));

builder.Services
      .AddFastEndpoints(o =>
      {
        o.Assemblies =
        [
          Assembly.GetAssembly(typeof(FeaturesExtension))!
        ];
      })
    .SwaggerDocument()
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
  c.Endpoints.RoutePrefix = "api";
  c.Serializer.Options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
})
.UseSwaggerGen();

app.Run();