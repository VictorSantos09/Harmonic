using Harmonic.Domain.Entities.Pais;
using QuickKit.Repositories.Contracts;

namespace Harmonic.Regras.Contracts.Repositories.Pais;

public interface IPaisGetRepository : IGetAllRepository<PaisEntity>, IGetByIdRepository<PaisEntity, int>
{
}
