using Dapper;
using System.Data;

namespace Harmonic.Shared.Data;

public interface IRepository
{
    void BeginTransaction();
    void Commit();
    void Rollback();
}

public abstract class Repository : IRepository
{
    protected IDbConnection _connection;

    protected Repository(IDbConnection conn)
    {
        _connection = conn;
    }

    private protected IDbTransaction _transaction;

    public IDbConnection Open()
    {
        if (_connection is null || _connection?.State == ConnectionState.Closed)
        {
            _connection.Open();
        }

        return _connection;
    }

    public void Close()
    {
        _connection.Close();
    }

    public void BeginTransaction()
    {
        Open();
        _transaction = _connection.BeginTransaction();
    }

    public void Commit()
    {
        _transaction.Commit();
        Close();
    }

    public void Rollback()
    {
        _transaction.Rollback();
        Close();
    }

    public async Task<bool> ExistsAsync<TKey>(string tableName, TKey id, CancellationToken cancellationToken)
    {
        string sql = $"SELECT IF((SELECT COUNT(1) FROM {tableName} WHERE ID = @Id), true, false) AS RESULT;";

        CommandDefinition command = new(sql, new
        {
            Id = id
        }, cancellationToken: cancellationToken);

        return await _connection.ExecuteScalarAsync<bool>(command);
    }
}
