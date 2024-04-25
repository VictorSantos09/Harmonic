using Harmonic.Domain.Entities.ConteudoPlataforma;
using QuickKit.Repositories.Contracts;

namespace Harmonic.Infra.Repositories.Contracts.ConteudoPlataforma;

public interface IConteudoPlataformaGetRepository :
                IGetAllRepository<ConteudoPlataformaEntity>,
                IGetByIdRepository<ConteudoPlataformaEntity, int>
{
}
