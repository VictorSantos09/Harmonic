namespace Harmonic.Regras.Contracts.Repositories.Common;

public interface IExistsRepository
{
    Task<bool> ExistsAsync(TABLES table, int id, CancellationToken cancellationToken);
}
