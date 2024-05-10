using Harmonic.Domain.Entities.Pais;
using QuickKit.Repositories.Contracts;

namespace Harmonic.Infra.Repositories.Pais.Contracts;

public interface IAdicionarPaisRepository : IAddRepository<PaisEntity>
{
    Task<bool> ExistsByName(string name, CancellationToken cancellationToken);
}
