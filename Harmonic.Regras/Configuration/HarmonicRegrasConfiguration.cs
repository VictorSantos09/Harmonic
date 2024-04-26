using Harmonic.Shared.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;

namespace Harmonic.Regras.Configuration;

public static class HarmonicRegrasConfiguration
{
    public static IServiceCollection AddRegras(this IServiceCollection services)
    {
        services.AddDependencies(typeof(HarmonicRegrasConfiguration).Assembly);

        return services;
    }
}
