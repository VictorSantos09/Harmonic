using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Harmonic.Domain.Configuration;

public static class DomainConfiguration
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining(typeof(DomainConfiguration));
        return services;
    }
}
