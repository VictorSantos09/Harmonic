using Harmonic.Domain.Entities.Plataforma;
using Harmonic.Regras.Services.Conteudo.DTOs;
using QuickKit.ResultTypes.Services.Contracts;

namespace Harmonic.Regras.Services.Plataforma.Contracts;

public interface IPlataformaAdicionarService : IAddService<PlataformaDTO>
{

}


public interface IPlataformaDeletarService : IDeleteService<int>
{
}

public interface IPlataformaAtualizarService : IUpdateService<PlataformaDTO>
{

}

public interface IPlataformaGetService : IGetAllService<PlataformaEntity>, IGetByIdService<PlataformaEntity, int>
{
    Task<PlataformaEntity?> GetByNameAsync(string nome, CancellationToken cancellationToken);

}
