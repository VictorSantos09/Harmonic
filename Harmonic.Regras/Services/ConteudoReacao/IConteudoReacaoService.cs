using Harmonic.Domain.Entities.Conteudo;
using Harmonic.Regras.Services.ConteudoReacao.DTO;
using QuickKit.ResultTypes;
using QuickKit.ResultTypes.Services.Contracts;

namespace Harmonic.Regras.Services.ConteudoReacao;

public interface IConteudoReacaoService : IAddService<ConteudoReacaoDTO>, IDeleteService<int>, IUpdateService<ConteudoReacaoDTO>
{
    Task<IFinal<ConteudoReacaoEntity>> GetUsuarioConteudoReacaoAsync(string idUsuario, int idConteudo, CancellationToken cancellationToken);
}
