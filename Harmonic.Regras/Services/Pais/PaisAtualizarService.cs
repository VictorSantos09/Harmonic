using FluentValidation;
using Harmonic.Domain.Entities.Pais;
using Harmonic.Infra.Enums;
using Harmonic.Infra.Repositories.Common;
using Harmonic.Infra.Repositories.Pais.Contracts;
using Harmonic.Regras.Services.Conteudo.DTOs;
using Harmonic.Regras.Services.Pais.Contracts;
using QuickKit.ResultTypes;

namespace Harmonic.Regras.Services.Pais;

internal class PaisAtualizarService : IPaisAtualizarService
{
    private readonly IPaisAtualizarRepository _paisAtualizarRepository;
    private readonly IExistsRepository _existsRepository;
    private readonly IValidator<PaisEntity> _validator;

    public PaisAtualizarService(IPaisAtualizarRepository paisAtualizarRepository,
                                IExistsRepository existsRepository,
                                IValidator<PaisEntity> validator)
    {
        _paisAtualizarRepository = paisAtualizarRepository;
        _existsRepository = existsRepository;
        _validator = validator;
    }

    public async Task<IFinal> UpdateAsync(PaisDTO dto, CancellationToken cancellationToken)
    {
        var exists = await _existsRepository.ExistsAsync(TABLES.PAISES, dto.Id, cancellationToken);

        if (!exists) return Final.Failure("pais.atualizar.NaoExiste", "o país não foi encontrado");

        PaisEntity pais = new(dto.Nome);

        var validationResult = await _validator.ValidateAsync(pais, cancellationToken);

        if (!validationResult.IsValid) return Final.Failure("pais.atualizar.Invalido", "dados inválidos foram informados");

        var result = await _paisAtualizarRepository.UpdateAsync(pais, cancellationToken);

        if (result > 0) return Final.Success("país atualizado com sucesso");

        return Final.Failure("pais.atualizar.Falha", "não foi possível atualizar o país");
    }
}
