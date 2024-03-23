using Microsoft.Extensions.DependencyInjection;

namespace BuildVersionsApi.Features;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using BuildVersionsApi.Features.Persistance.Context;
using BuildVersionsApi.Features.Persistance.Service;

public static class FeaturesExtension
{
  public static IServiceCollection AddFeatures(IServiceCollection services, Func<string> connectionStringFunc = null)
  {
    var connectionString = connectionStringFunc?.Invoke()
      ?? throw new ArgumentNullException("ConnectionString not provided!");
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
