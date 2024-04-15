using Harmonic.Domain.Entities.Pais;
using Harmonic.Regras.Services.Conteudo.DTOs;
using QuickKit.ResultTypes.Services.Contracts;

namespace Harmonic.Regras.Services.Pais.Contracts;

public interface IPaisAdicionarService : IAddService<PaisDTO>
{

}

public interface IPaisDeletarService : IDeleteService<int>
{
    
}

public interface IPaisAtualizarService : IUpdateService<PaisDTO>
{
    
}

public interface IPaisGetService : IGetAllService<PaisEntity>, IGetByIdService<PaisEntity, int>
{
    
}