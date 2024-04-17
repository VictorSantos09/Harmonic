using Microsoft.Extensions.DependencyInjection;
using Scrutor;

namespace Harmonic.Regras.Configuration;

public static class HarmonicRegrasConfiguration
{
    public static IServiceCollection AddRegras(this IServiceCollection services)
    {
        services.Scan(scan => scan.FromCallingAssembly()
        .AddClasses(classes => classes.Where(c => !c.Name.EndsWith("DTO")), false)
        .UsingRegistrationStrategy(RegistrationStrategy.Skip)
        .AsMatchingInterface()
        .AsImplementedInterfaces()
        .WithTransientLifetime());

        return services;
    }
}
