using Harmonic.Domain.Entities.Conteudo;
using QuickKit.Repositories.Contracts;

namespace Harmonic.Regras.Contracts.Repositories.Conteudo;

public interface IConteudoGetRepository : IGetAllRepository<ConteudoEntity>, IGetByIdRepository<ConteudoEntity, int>
{
    
}