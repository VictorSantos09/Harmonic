using Harmonic.Domain.Entities.Plataforma;
using Harmonic.Infra.Repositories.Plataforma.Contracts;
using Harmonic.Regras.Services.Conteudo.DTOs;
using Harmonic.Regras.Services.Plataforma.Contracts;
using QuickKit.ResultTypes;

namespace Harmonic.Regras.Services.Plataforma;

internal class PlataformaAdicionarService : IPlataformaAdicionarService
{
    private readonly IPlataformaAdicionarRepository _adicionarPlataformaRepository;

    public PlataformaAdicionarService(IPlataformaAdicionarRepository adicionarPlataformaRepository)
    {
        _adicionarPlataformaRepository = adicionarPlataformaRepository;
    }

    public async Task<IFinal> AddAsync(PlataformaDTO dto, CancellationToken cancellationToken)
    {

        PlataformaEntity entity = new(dto.Id, dto.Nome, dto.URL);

        int result = await _adicionarPlataformaRepository.AddAsync(entity, cancellationToken);
        if (result > 0) return Final.Success();
        return Final.Failure("Plataforma.Add.Falha", "Não foi possível adicionar o plataforma");
    }
}
