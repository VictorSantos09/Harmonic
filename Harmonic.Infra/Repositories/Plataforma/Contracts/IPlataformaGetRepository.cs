using Harmonic.Domain.Entities.Plataforma;
using QuickKit.Repositories.Contracts;

namespace Harmonic.Infra.Repositories.Plataforma.Contracts;

public interface IPlataformaGetRepository : IGetAllRepository<PlataformaEntity>, IGetByIdRepository<PlataformaEntity, int>
{
    Task<PlataformaEntity> GetByNameAsync(string nome, CancellationToken cancellationToken);
}
