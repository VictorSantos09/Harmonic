using Harmonic.Domain.Entities.Conteudo;
using QuickKit.Repositories.Contracts;

namespace Harmonic.Infra.Repositories.Conteudo.Contracts;

public interface IConteudoGetRepository : IGetAllRepository<ConteudoEntity>, IGetByIdRepository<ConteudoEntity, int>
{

}