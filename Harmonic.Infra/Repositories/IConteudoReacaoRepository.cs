using Harmonic.Domain.Entities.ConteudoReacao;
using QuickKit.Repositories.Contracts;

namespace Harmonic.Infra.Repositories;

public interface IConteudoReacaoRepository : IAddRepository<ConteudoReacaoEntity>, IUpdateRepository<ConteudoReacaoEntity>, IDeleteRepository<ConteudoReacaoEntity, int>
{
    Task<ConteudoReacaoEntity?> GetUsuarioConteudoReacaoAsync(string idUsuario, int idConteudo, CancellationToken cancellationToken);
    Task<IEnumerable<ConteudoReacaoEntity>> GetUsuarioConteudoReacaoAsync(string idUsuario, CancellationToken cancellationToken);
}
