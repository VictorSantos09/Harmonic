using FluentValidation;
using Harmonic.Domain.Entities.Plataforma;
using Harmonic.Infra.Repositories.Plataforma.Contracts;
using Harmonic.Regras.Services.Conteudo.DTOs;
using Harmonic.Regras.Services.Plataforma.Contracts;
using QuickKit.ResultTypes;


namespace Harmonic.Regras.Services.Plataforma;

internal class PlataformaAtualizarService : IPlataformaAtualizarService
{
    private readonly IPlataformaAtualizarRepository _plataformaAtualizarRepository;
    private readonly IValidator<PlataformaEntity> _validator;

    public PlataformaAtualizarService(IPlataformaAtualizarRepository plataformaAtualizarRepository,
                                    IValidator<PlataformaEntity> validator)
    {
        _plataformaAtualizarRepository = plataformaAtualizarRepository;
        _validator = validator;
    }

    public async Task<IFinal> UpdateAsync(PlataformaDTO dto, CancellationToken cancellationToken)
    {
        PlataformaEntity plataforma = new(dto.Id, dto.Nome, dto.URL) { Id = dto.Id };

        var validationResult = await _validator.ValidateAsync(plataforma, cancellationToken);

        if (!validationResult.IsValid) return Final.Failure("plataforma.atualizar.Invalido", "dados do plataforma são inválidos");

        var result = await _plataformaAtualizarRepository.UpdateAsync(plataforma, cancellationToken);

        if (result > 0) return Final.Success("conteúdo atualizado com sucesso");

        return Final.Failure("plataforma.atualizar.Falha", "não foi possível atualizar o plataforma");
    }
}
