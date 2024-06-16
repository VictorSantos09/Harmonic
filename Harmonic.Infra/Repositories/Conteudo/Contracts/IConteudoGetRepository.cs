using Harmonic.Domain.Entities.Conteudo;
using QuickKit.Repositories.Contracts;

namespace Harmonic.Infra.Repositories.Conteudo.Contracts;

public interface IConteudoGetRepository : IGetAllRepository<ConteudoEntity>, IGetByIdRepository<ConteudoEntity, int>
{
    Task<IEnumerable<ConteudoDetalhesDto>> GetAllDetalhesAsync(CancellationToken cancellationToken);
    Task<IEnumerable<string>> GetConteudoPlataformasURL(int id, CancellationToken cancellationToken);
    Task<ConteudoDetalhesDto?> GetDetalhesAsync(int id, CancellationToken cancellationToken);
    Task<IEnumerable<ConteudoTopEntity>> GetTopPodcastsAsync(CancellationToken cancellationToken);
    Task<IEnumerable<ConteudoTopEntity>> GetTopRadiosAsync(CancellationToken cancellationToken);
}