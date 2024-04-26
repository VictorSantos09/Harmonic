using Harmonic.Domain.Entities.Plataforma;
using QuickKit.Repositories.Contracts;

namespace Harmonic.Infra.Repositories.Contracts.Plataforma;

public interface IPlataformaGetRepository : IGetAllRepository<PlataformaEntity>, IGetByIdRepository<PlataformaEntity, int>
{
}
