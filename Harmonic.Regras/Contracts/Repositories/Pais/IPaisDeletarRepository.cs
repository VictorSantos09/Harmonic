using Harmonic.Domain.Entities.Pais;
using QuickKit.Repositories.Contracts;

namespace Harmonic.Regras.Contracts.Repositories.Pais;

public interface IPaisDeletarRepository : IDeleteRepository<PaisEntity, int>
{
}
