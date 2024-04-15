using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Harmonic.Shared.Configuration;

public static class DependencyInjectionConfiguration
{
    public static IServiceCollection AddDependencies(this IServiceCollection services)
    {
        services.Scan(scan => scan.FromCallingAssembly()
        .AddClasses()
        .AsMatchingInterface());

        return services;
    }

    public static IServiceCollection AddDependencies(this IServiceCollection services, Assembly assembly)
    {
        services.Scan(scan => scan.FromAssemblies(assembly)
        .AddClasses()
        .AsMatchingInterface());

        return services;
    }
}
