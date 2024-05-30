using Harmonic.Domain.Entities.ConteudoReacao;
using Harmonic.Domain.Entities.ConteudoReacao.DTOs;
using Harmonic.Regras.Services.ConteudoReacao.DTO;
using QuickKit.ResultTypes;
using QuickKit.ResultTypes.Services.Contracts;

namespace Harmonic.Regras.Services.ConteudoReacao;

public interface IConteudoReacaoService : IAddService<ConteudoReacaoDTO>, IUpdateService<ConteudoReacaoDTO>
{
    Task<IFinal<bool>> GetUsuarioConteudoReacaoAsync(string idUsuario, int idConteudo, CancellationToken cancellationToken);
    Task<IFinal> DeleteAsync(string idUsuario, int idConteudo, CancellationToken cancellationToken);
    Task<IFinal<IEnumerable<ConteudoReacaoEntity>>> GetUsuarioConteudoReacaoAsync(string idUsuario, CancellationToken cancellationToken = default);
    Task<IFinal<IEnumerable<UsuarioConteudoCurtidoDTO>>> GetUsuarioConteudosCurtidosAsync(string idUsuario, CancellationToken cancellationToken = default);
}
