using Harmonic.Domain.Entities.TipoConteudo;
using QuickKit.Repositories.Contracts;

namespace Harmonic.Infra.Repositories.TipoConteudo.Contracts
{

    public interface ITipoConteudoDeletarRepository : IDeleteRepository<TipoConteudoEntity, int> { }

}