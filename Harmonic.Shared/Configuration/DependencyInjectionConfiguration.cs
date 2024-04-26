using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Harmonic.Shared.Configuration;

public static class DependencyInjectionConfiguration
{
    public static IServiceCollection AddDependencies(this IServiceCollection services)
    {
        services.Scan(scan => scan.FromCallingAssembly()
         .AddClasses(classes => classes.Where(c => c.Name.ToUpper().EndsWith("REPOSITORY") || c.Name.ToUpper().EndsWith("SERVICE")), false)
         .AsImplementedInterfaces()
         .WithTransientLifetime());

        return services;
    }

    public static IServiceCollection AddDependencies(this IServiceCollection services, Assembly assembly)
    {
        services.Scan(scan => scan.FromAssemblies(assembly)
       .AddClasses(classes => classes.Where(c => c.Name.ToUpper().EndsWith("REPOSITORY") || c.Name.ToUpper().EndsWith("SERVICE")), false)
       .AsImplementedInterfaces()
       .WithTransientLifetime());

        return services;
    }
}
