using Harmonic.Domain.Entities.ConteudoPlataforma;
using Harmonic.Infra.Repositories.ConteudoPlataforma.Contracts;
using Harmonic.Regras.Services.ConteudoPlataforma.Contracts;
using Harmonic.Regras.Services.ConteudoPlataforma.DTOs;
using QuickKit.ResultTypes;

namespace Harmonic.Regras.Services.ConteudoPlataforma;

internal class ConteudoPlataformaAdicionarService : IConteudoPlataformaAdicionarService
{
    private readonly IConteudoPlataformaAdicionarRepository _adicionarRepository;

    public ConteudoPlataformaAdicionarService(IConteudoPlataformaAdicionarRepository adicionarRepository)
    {
        _adicionarRepository = adicionarRepository;
    }

    public async Task<IFinal> AddAsync(ConteudoPlataformaDTO dto, CancellationToken cancellationToken)
    {
        ConteudoPlataformaEntity conteudoPlataforma = new(dto.URL, dto.Conteudo, dto.Plataforma) { Id = dto.Id };

        int result = await _adicionarRepository.AddAsync(conteudoPlataforma, cancellationToken);
        if (result > 0) return Final.Success();
        return Final.Failure("ConteudoPlataforma.Add.Falha", "Falha ao inserir registro");

    }
}
