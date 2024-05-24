namespace Microsoft.Extensions.DependencyInjection;

using BuildVersionsApi.Domain.Abstract;
using BuildVersionsApi.Persistance.Context;
using BuildVersionsApi.Persistance.Interceptor;
using BuildVersionsApi.Persistance.Service;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

public static class PersistanceExtension
{
  public static IServiceCollection AddBuildVersionsApiPersistance(this IServiceCollection services, string? connectionString)
  {
    ArgumentNullException.ThrowIfNull(nameof(connectionString));

    ServerVersion serverVersion = ServerVersion.AutoDetect(connectionString);

    _ = services.AddSingleton<PerformanceInterceptor>();
    _ = services.AddSingleton<SetCreatedOrUpdatedInterceptor>();
    _ = services.AddSingleton<SoftDeleteInterceptor>();

    _ = services.AddDbContext<BuildVersionsDbContext>((sp, options) =>
      options.UseMySql(connectionString, serverVersion)
      .AddInterceptors(sp.GetRequiredService<PerformanceInterceptor>())
      .AddInterceptors(sp.GetRequiredService<SetCreatedOrUpdatedInterceptor>())
      .AddInterceptors(sp.GetRequiredService<SoftDeleteInterceptor>())
      );

    _ = services.AddTransient<IStorageService, StorageService>();

    return services;
  }

  public static IHost ConfigurePersistance(this IHost app)
  {
    using IServiceScope scope = app.Services.CreateScope();
    _ = scope.ServiceProvider
      .GetRequiredService<BuildVersionsDbContext>()
      .EnsureDbExists();

    return app;
  }
}