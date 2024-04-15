using Harmonic.Domain.Configuration;
using Harmonic.Regras.Services.Pais.Add;
using Microsoft.Extensions.DependencyInjection;

namespace Harmonic.Regras.Configuration;

public static class HarmonicRegrasConfiguration
{
    public static IServiceCollection AddRegras(this IServiceCollection services)
    {
        services.AddDomain();
        services.AddTransient<IAdicionarPaisService, AdicionarPaisService>();
        return services;
    }
}
