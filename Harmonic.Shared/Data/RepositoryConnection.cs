using Microsoft.Extensions.Configuration;

namespace Harmonic.Shared.Data;
public static class RepositoryConnection
{
    public static bool UseDevelopment = true;
    public static string? UseConnectionString;

    public static string? GetConnectionString(this IConfiguration configuration)
    {
        if (!string.IsNullOrEmpty(UseConnectionString)) return configuration.GetConnectionString(UseConnectionString);

        if (UseDevelopment) return configuration.GetConnectionString("Development");
        return configuration.GetConnectionString("Production");
    }
}
