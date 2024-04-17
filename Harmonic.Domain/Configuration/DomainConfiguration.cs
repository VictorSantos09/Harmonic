using FluentValidation;
using Harmonic.Domain.Entities.Conteudo;
using Harmonic.Domain.Entities.Pais;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using System.Reflection;

namespace Harmonic.Domain.Configuration;

public static class DomainConfiguration
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        //services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddTransient<IValidator<PaisEntity>, PaisValidator>();

        services.AddTransient<IValidator<ConteudoEntity>, ConteudoValidator>();

        return services;
    }
}
