using Dapper;
using Harmonic.Infra.Enums;
using Harmonic.Shared.Data;
using System.Data;

namespace Harmonic.Infra.Repositories.Common;

internal class ExistsRepository(IDbConnection conn) : Repository(conn), IExistsRepository
{
    public async Task<bool> ExistsAsync(TABLES table, int id, CancellationToken cancellationToken)
    {
        string sql = $"SELECT IF((SELECT COUNT(1) FROM {table} WHERE ID = @Id), true, false) AS RESULT;";

        CommandDefinition command = new(sql, new
        {
            Id = id
        }, cancellationToken: cancellationToken);

        return await _connection.ExecuteScalarAsync<bool>(command);
    }
}
