using Harmonic.Shared.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuickKit.Configuration;

namespace Harmonic.Infra.Configuration;

public static class HarmonicInfraConfiguration
{
    public static IServiceCollection AddInfra(this IServiceCollection services)
    {
        services.AddProcedureNameBuildersFromAssembly();
        services.AddDependencies(typeof(HarmonicInfraConfiguration).Assembly);

        return services;
    }
}
