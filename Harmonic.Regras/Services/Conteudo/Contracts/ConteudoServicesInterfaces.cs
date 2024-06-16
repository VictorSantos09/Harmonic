using Harmonic.Domain.Entities.Conteudo;
using Harmonic.Regras.Services.Conteudo.DTOs;
using QuickKit.ResultTypes;
using QuickKit.ResultTypes.Services.Contracts;

namespace Harmonic.Regras.Services.Conteudo.Contracts;

public interface IConteudoAdicionarService : IAddService<ConteudoDTO>
{
}


public interface IConteudoDeletarService : IDeleteService<int>
{
    Task DeleteRangeAsync(IEnumerable<int> ids, CancellationToken cancellationToken);
}

public interface IConteudoAtualizarService : IUpdateService<ConteudoDTO>
{

}

public interface IConteudoGetService : IGetAllService<ConteudoEntity>, IGetByIdService<ConteudoEntity, int>
{
    Task<IFinal<IEnumerable<ConteudoDetalhesDto>>> GetAllDetalhesAsync(CancellationToken cancellationToken);
    Task<IFinal<IEnumerable<string>>> GetConteudoPlataformasURL(int id, CancellationToken cancellationToken);
    Task<IFinal<ConteudoDetalhesDto>> GetDetalhesAsync(int id, CancellationToken cancellationToken);
    Task<IFinal<IEnumerable<ConteudoTopEntity>>> GetTopPodcastsAsync(CancellationToken cancellationToken);
    Task<IFinal<IEnumerable<ConteudoTopEntity>>> GetTopRadiosAsync(CancellationToken cancellationToken);
}
