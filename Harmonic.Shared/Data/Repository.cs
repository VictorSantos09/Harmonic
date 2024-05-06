using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;

namespace Harmonic.Shared.Data;

public abstract class Repository
{
    private readonly IConfiguration _configuration;

    protected Repository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected IDbConnection Connect()
    {
        var connectionString = _configuration.GetConnectionString();
        return new MySqlConnection(connectionString);
    }
}
