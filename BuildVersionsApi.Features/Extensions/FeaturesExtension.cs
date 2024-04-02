namespace Microsoft.Extensions.DependencyInjection;

using System.Reflection;

public static class FeaturesExtension
{
    public static IServiceCollection AddBuildVersionsApiFeatures(this IServiceCollection services)
    {
        Assembly assembly = Assembly.GetExecutingAssembly();

        _ = services.AddMediatR(config => config.RegisterServicesFromAssembly(assembly))
          .AddAutoMapper(assembly);

        return services;
    }
}