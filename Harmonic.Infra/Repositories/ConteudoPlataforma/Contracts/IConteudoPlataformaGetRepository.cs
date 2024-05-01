using Harmonic.Domain.Entities.ConteudoPlataforma;
using QuickKit.Repositories.Contracts;

namespace Harmonic.Infra.Repositories.ConteudoPlataforma.Contracts;

public interface IConteudoPlataformaGetRepository :
                IGetAllRepository<ConteudoPlataformaEntity>,
                IGetByIdRepository<ConteudoPlataformaEntity, int>
{
}
