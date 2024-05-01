using Dapper;
using Harmonic.Infra.Enums;
using Harmonic.Shared.Data;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Harmonic.Infra.Repositories.Common;

internal class ExistsRepository(IConfiguration configuration) : Repository(configuration), IExistsRepository
{
    public async Task<bool> ExistsAsync(TABLES table, int id, CancellationToken cancellationToken)
    {
        string sql = $"SELECT IF((SELECT COUNT(1) FROM {table} WHERE ID = @Id), true, false) AS RESULT;";

        CommandDefinition command = new(sql, new
        {
            Id = id
        }, cancellationToken: cancellationToken);

        using IDbConnection conn = Connect();
        return await conn.ExecuteScalarAsync<bool>(command);
    }
}
