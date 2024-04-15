using Harmonic.Infra.Repositories.Conteudo;
using Harmonic.Infra.Repositories.Pais.Add;
using Harmonic.Regras.Contracts.Repositories.Conteudo;
using Harmonic.Regras.Contracts.Repositories.Pais;
using Microsoft.Extensions.DependencyInjection;
using QuickKit.Configuration;

namespace Harmonic.Infra.Configuration;

public static class HarmonicInfraConfiguration
{
    public static IServiceCollection AddInfra(this IServiceCollection services)
    {
        services.AddProcedureNameBuildersFromAssembly();
        services.AddTransient<IAdicionarPaisRepository, AdicionarPaisRepository>();
        services.AddTransient<IAdicionarConteudoRepository, AdicionarConteudoRepository>();
        return services;
    }
}
