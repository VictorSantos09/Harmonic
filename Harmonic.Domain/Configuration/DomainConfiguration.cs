using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Harmonic.Domain.Configuration;

public static class DomainConfiguration
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}
