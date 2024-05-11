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
    protected readonly IDbConnection _connection;
    private IDbTransaction _transaction;

    protected Repository(IDbConnection connection)
    {
        _connection = connection;
    }

    public void BeginTransaction()
    {
        _connection.Open();
        _transaction = _connection.BeginTransaction();
    }

    public void Commit()
    {
        _transaction.Commit();
    }

    public void Rollback()
    {
        _transaction.Rollback();
    }
}
