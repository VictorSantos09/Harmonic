using Harmonic.Domain.Entities.Conteudo;
using Harmonic.Shared.Data;
using QuickKit.Repositories.Contracts;

namespace Harmonic.Infra.Repositories.Conteudo.Contracts;

public interface IConteudoDeletarRepository : IRepository, IDeleteRepository<ConteudoEntity, int>
{
    Task DeleteRangeAsync(IEnumerable<int> ids, CancellationToken cancellationToken);
}
