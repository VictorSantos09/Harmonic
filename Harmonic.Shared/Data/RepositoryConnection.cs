using Microsoft.Extensions.Configuration;

namespace Harmonic.Shared.Data;
public static class RepositoryConnection
{
    public static bool UseDevelopment = true;
    public static string? UseConnectionString;

    public static string? GetConnectionString(this IConfiguration configuration)
    {
        var connection = Environment.GetEnvironmentVariable("DATABASE_CONNECTION");

        if(connection is not null)
        {
            Console.WriteLine("connection found");
            return connection;
        }

        if (!string.IsNullOrEmpty(UseConnectionString)) return configuration.GetConnectionString(UseConnectionString);

        if (UseDevelopment) return configuration.GetConnectionString("Development");
        return configuration.GetConnectionString("Production");
    }
}
