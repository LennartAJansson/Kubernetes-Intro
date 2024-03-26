namespace BuildVersionsApi.Features;
using System.Reflection;

using BuildVersionsApi.Features.Persistance.Context;
using BuildVersionsApi.Features.Persistance.Service;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public static class FeaturesExtension
{
  public static IServiceCollection AddBuildVersionsFeatures(this IServiceCollection services, string? connectionString)
  {
    ServerVersion serverVersion = ServerVersion.AutoDetect(connectionString);
    Assembly assembly = Assembly.GetExecutingAssembly();

    _ = services.AddDbContext<BuildVersionsDbContext>(options => options.UseMySql(connectionString, serverVersion))
      .AddTransient<IPersistanceService, PersistanceService>()
      .AddMediatR(config => config.RegisterServicesFromAssembly(assembly))
      .AddAutoMapper(assembly);

    return services;
  }

  public static IHost ConfigurePersistance(this IHost app)
  {
    using IServiceScope scope = app.Services.CreateScope();
    _ = scope.ServiceProvider.GetRequiredService<BuildVersionsDbContext>().EnsureDbExists();

    return app;
  }
}
