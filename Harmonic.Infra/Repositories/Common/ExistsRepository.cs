using Dapper;
using Harmonic.Regras.Contracts.Repositories.Common;
using Harmonic.Regras.Enums;
using Harmonic.Shared.Data;
using System.Data;

namespace Harmonic.Infra.Repositories.Common;

internal class ExistsRepository : Repository, IExistsRepository
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
