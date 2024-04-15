using Harmonic.Domain.Entities.Conteudo;
using Harmonic.Regras.Services.Conteudo.DTOs;
using QuickKit.ResultTypes.Services.Contracts;

namespace Harmonic.Regras.Services.Conteudo.Contracts;

public interface IConteudoAdicionarService : IAddService<ConteudoDTO>
{

}


public interface IConteudoDeletarService : IDeleteService<int>
{
}

public interface IConteudoAtualizarService : IUpdateService<ConteudoDTO>
{

}

public interface IConteudoGetService : IGetAllService<ConteudoEntity>, IGetByIdService<ConteudoEntity, int>
{

}
