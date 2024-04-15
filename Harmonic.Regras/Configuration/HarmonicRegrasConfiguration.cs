using Microsoft.Extensions.DependencyInjection;

namespace Harmonic.Regras.Configuration;

public static class HarmonicRegrasConfiguration
{
    public static IServiceCollection AddRegras(this IServiceCollection services)
    {
        services.Scan(scan => scan
     .FromCallingAssembly()
     .AddClasses(false)
     .AsMatchingInterface()
     .WithTransientLifetime());
        return services;
    }
}
