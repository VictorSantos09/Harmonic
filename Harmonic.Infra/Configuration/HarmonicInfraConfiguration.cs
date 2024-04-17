using Microsoft.Extensions.DependencyInjection;
using QuickKit.Configuration;
using Scrutor;

namespace Harmonic.Infra.Configuration;

public static class HarmonicInfraConfiguration
{
    public static IServiceCollection AddInfra(this IServiceCollection services)
    {
        services.AddProcedureNameBuildersFromAssembly();

        services.Scan(scan => scan.FromCallingAssembly()
        .AddClasses(false)
        .UsingRegistrationStrategy(RegistrationStrategy.Skip)
        .AsMatchingInterface()
        .AsImplementedInterfaces()
        .WithTransientLifetime());

        return services;
    }
}
