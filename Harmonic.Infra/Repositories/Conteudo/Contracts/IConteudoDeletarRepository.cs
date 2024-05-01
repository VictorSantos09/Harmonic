using Harmonic.Domain.Entities.Conteudo;
using QuickKit.Repositories.Contracts;

namespace Harmonic.Infra.Repositories.Conteudo.Contracts;

public interface IConteudoDeletarRepository : IDeleteRepository<ConteudoEntity, int> { }
