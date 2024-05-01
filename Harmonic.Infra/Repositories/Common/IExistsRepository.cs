using Harmonic.Infra.Enums;

namespace Harmonic.Infra.Repositories.Common;

public interface IExistsRepository
{
    Task<bool> ExistsAsync(TABLES table, int id, CancellationToken cancellationToken);
}
