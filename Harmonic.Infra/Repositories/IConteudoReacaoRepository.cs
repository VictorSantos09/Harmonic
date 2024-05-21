using Harmonic.Domain.Entities.Conteudo;
using QuickKit.Repositories.Contracts;

namespace Harmonic.Infra.Repositories;

public interface IConteudoReacaoRepository : IAddRepository<ConteudoReacaoEntity>, IUpdateRepository<ConteudoReacaoEntity>, IDeleteRepository<ConteudoReacaoEntity, int>
{
    Task<ConteudoReacaoEntity?> GetUsuarioConteudoReacaoAsync(string idUsuario, int idConteudo, CancellationToken cancellationToken);
}
