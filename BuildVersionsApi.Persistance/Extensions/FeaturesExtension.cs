namespace Microsoft.Extensions.DependencyInjection;

using System.Reflection;

using BuildVersionsApi.Domain.Abstract;
using BuildVersionsApi.Persistance.Context;
using BuildVersionsApi.Persistance.Service;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

public static class PersistanceExtension
{
  public static IServiceCollection AddBuildVersionsApiPersistance(this IServiceCollection services, string? connectionString)
  {
    ArgumentNullException.ThrowIfNull(nameof(connectionString));

    ServerVersion serverVersion = ServerVersion.AutoDetect(connectionString);
    Assembly assembly = Assembly.GetExecutingAssembly();

    _ = services.AddDbContext<BuildVersionsDbContext>(options => options.UseMySql(connectionString, serverVersion));
    services.AddTransient<IStorageService, StorageService>()
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