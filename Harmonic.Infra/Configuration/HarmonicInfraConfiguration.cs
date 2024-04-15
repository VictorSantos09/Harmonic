using FluentValidation;
using Harmonic.Domain.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuickKit.Configuration;

namespace Harmonic.Infra.Configuration;

public static class HarmonicInfraConfiguration
{
    public static IServiceCollection AddInfra(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining(typeof(DomainConfiguration));
        services.AddProcedureNameBuildersFromAssembly();

        services.Scan(scan => scan
      .FromCallingAssembly()
      .AddClasses(false)
      .AsMatchingInterface()
      .WithTransientLifetime());

        return services;
    }
}
