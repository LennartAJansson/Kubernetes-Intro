namespace BuildVersionsApi.Domain.Extensions;

using BuildVersionsApi.Domain.Abstract;
using BuildVersionsApi.Domain.Services;

using Microsoft.Extensions.DependencyInjection;

public static class DomainExtension
{
  public static IServiceCollection AddBuildVersionsApiDomain(this IServiceCollection services)
  {
    _ = services.AddTransient<IDomainService, DomainService>();
    return services;
  }
}