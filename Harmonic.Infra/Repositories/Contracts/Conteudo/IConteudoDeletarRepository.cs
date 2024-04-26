using Harmonic.Domain.Entities.Conteudo;
using QuickKit.Repositories.Contracts;

namespace Harmonic.Infra.Repositories.Contracts.Conteudo;

public interface IConteudoDeletarRepository : IDeleteRepository<ConteudoEntity, int> { }
