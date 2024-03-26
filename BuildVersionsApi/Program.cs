using Microsoft.Extensions.DependencyInjection;
using BuildVersionsApi.Features;
using FastEndpoints;
using System.Reflection;
using FastEndpoints.Swagger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
  .AddBuildVersionsFeatures(builder.Configuration.GetConnectionString("DefaultConnection"));

builder.Services
      .AddFastEndpoints(o => {
        o.Assemblies =
        [
          Assembly.GetAssembly(typeof(FeaturesExtension))!
        ];
      })
    .SwaggerDocument()
    .AddResponseCaching();

var app = builder.Build();

app.ConfigurePersistance();

app.UseHttpsRedirection();

app
.UseResponseCaching()
.UseDefaultExceptionHandler()
.UseFastEndpoints()
.UseSwaggerGen(); 

app.Run();
