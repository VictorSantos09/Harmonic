using Harmonic.Domain.Entities.TipoConteudo;
using Harmonic.Regras.Services.Conteudo.DTOs;
using QuickKit.ResultTypes.Services.Contracts;

namespace Harmonic.Regras.Services.TipoConteudo.Contracts
{
    public interface ITipoConteudoAdicionarServices : IAddService<TipoConteudoDTO>
    {
    }

    public interface ITipoConteudoAtualizarServices : IUpdateService<TipoConteudoDTO> 
    {
    }

    public interface ITipoConteudoDeletarServices : IDeleteService<int> 
    {
    }

    public interface ITipoConteudoGetServices : IGetAllService<TipoConteudoEntity>, IGetByIdService<TipoConteudoEntity, int>
    { 
    }

}
