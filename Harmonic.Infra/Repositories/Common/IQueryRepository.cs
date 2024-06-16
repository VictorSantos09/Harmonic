using Dapper;
using Harmonic.Shared.Data;
using System.Data;

namespace Harmonic.Infra.Repositories.Common;
public interface IQueryRepository
{
    Task<T?> QuerySingleOrDefaultAsync<T>(string query, object? parameters = null);
}

public class QueryRepository : Repository, IQueryRepository
{
    public QueryRepository(IDbConnection conn) : base(conn)
    {
    }

    public Task<T?> QuerySingleOrDefaultAsync<T>(string query, object? parameters = null)
    {
        return _connection.QuerySingleOrDefaultAsync<T>(query, parameters);
    }
}
