using BuildVersionsApi.Features;
using FastEndpoints;
using System.Reflection;
using FastEndpoints.Swagger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
  .AddBuildVersionsFeatures(builder.Configuration.GetConnectionString("BuildVersionsDb"));

builder.Services
      .AddFastEndpoints(o => {
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
    policy.AllowAnyOrigin()
      .AllowAnyHeader()
      .AllowAnyMethod();
  });
});

var app = builder.Build();

app.ConfigurePersistance();

app.UseCors();

app.UseHttpsRedirection();

app
.UseResponseCaching()
.UseDefaultExceptionHandler()
.UseFastEndpoints()
.UseSwaggerGen(); 

app.Run();
