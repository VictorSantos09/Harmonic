﻿using Harmonic.Domain.Entities.ConteudoPlataforma;
using Harmonic.Domain.Entities.ConteudoPlataforma.DTOs;
using Harmonic.Regras.Services.ConteudoPlataforma.DTOs;
using QuickKit.ResultTypes;
using QuickKit.ResultTypes.Services.Contracts;

namespace Harmonic.Regras.Services.ConteudoPlataforma.Contracts;

public interface IConteudoPlataformaAdicionarService : IAddService<ConteudoPlataformaDTO>
{

}

public interface IConteudoPlataformaDeletarService : IDeleteService<int>
{

}

public interface IConteudoPlataformaAtualizarService : IUpdateService<ConteudoPlataformaDTO>
{

}

public interface IConteudoPlataformaGetService : IGetAllService<ConteudoPlataformaEntity>, IGetByIdService<ConteudoPlataformaEntity, int>
{
    Task<IFinal<IEnumerable<ConteudoPlataformaDetalhesDTO>>> GetDetalhesAsync(int id, CancellationToken cancellationToken);
}
