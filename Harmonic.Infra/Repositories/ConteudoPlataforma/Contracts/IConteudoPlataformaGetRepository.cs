using Harmonic.Domain.Entities.ConteudoPlataforma;
using Harmonic.Domain.Entities.ConteudoPlataforma.DTOs;
using QuickKit.Repositories.Contracts;

namespace Harmonic.Infra.Repositories.ConteudoPlataforma.Contracts;

public interface IConteudoPlataformaGetRepository :
                IGetAllRepository<ConteudoPlataformaEntity>,
                IGetByIdRepository<ConteudoPlataformaEntity, int>
{
    Task<IEnumerable<ConteudoPlataformaDetalhesDTO>> GetDetalhesAsync(int id, CancellationToken cancellationToken);
}
