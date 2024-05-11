using Harmonic.Domain.Entities.Pais;
using QuickKit.Repositories.Contracts;

namespace Harmonic.Infra.Repositories.Pais.Contracts;

public interface IPaisGetRepository : IGetAllRepository<PaisEntity>, IGetByIdRepository<PaisEntity, int>
{
}
