using Harmonic.Domain.Entities.Pais;
using QuickKit.Repositories.Contracts;

namespace Harmonic.Infra.Repositories.Contracts.Pais;

public interface IPaisGetRepository : IGetAllRepository<PaisEntity>, IGetByIdRepository<PaisEntity, int>
{
}
