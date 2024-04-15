using Microsoft.Extensions.DependencyInjection;
using QuickKit.AspNetCore.Swagger.Configuration;
using QuickKit.Configuration;
using System.Reflection;

namespace Harmonic.Shared.Extensions.Configuration;

public static class HarmonicDependenciesExtensions
{
    public static IServiceCollection AddRequiredDependecies(this IServiceCollection services, Assembly? assembly = null)
    {
        if (assembly is not null) services.AddSwaggerDocsFromXmlComments(assembly);
        services.AddProcedureNameBuildersFromAssembly();
        return services;
    }
}
