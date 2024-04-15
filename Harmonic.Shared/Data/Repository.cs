using MySql.Data.MySqlClient;
using System.Data;

namespace Harmonic.Shared.Data;

public abstract class Repository
{
    protected static IDbConnection Connect()
    {
        var connectionString = "Server=localhost;Database=harmonicDSV;Uid=root;Pwd=root;";
        return new MySqlConnection(connectionString);
    }
}
