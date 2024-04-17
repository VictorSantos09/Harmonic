using Harmonic.Infra.Enums;

namespace Harmonic.Infra.Repositories.Contracts.Common;

public interface IExistsRepository
{
    Task<bool> ExistsAsync(TABLES table, int id, CancellationToken cancellationToken);
}
