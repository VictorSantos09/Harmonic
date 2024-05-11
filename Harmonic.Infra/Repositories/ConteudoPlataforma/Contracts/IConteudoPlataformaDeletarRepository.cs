using Harmonic.Domain.Entities.ConteudoPlataforma;
using QuickKit.Repositories.Contracts;

namespace Harmonic.Infra.Repositories.ConteudoPlataforma.Contracts;

public interface IConteudoPlataformaDeletarRepository : IDeleteRepository<ConteudoPlataformaEntity, int>
{
}
