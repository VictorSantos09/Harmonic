using FluentValidation;
using Harmonic.Domain.Entities.Pais;
using Harmonic.Regras.Contracts.Repositories.Pais;
using Harmonic.Regras.Services.Conteudo.DTOs;
using QuickKit.ResultTypes;
using QuickKit.ResultTypes.Services.Contracts;

namespace Harmonic.Regras.Services.Pais.Add;

public interface IAdicionarPaisService : IAddService<PaisDTO>
{
    
}

internal class AdicionarPaisService : IAdicionarPaisService
{
    private readonly IAdicionarPaisRepository _adicionarPaisRepository;
    private readonly IValidator<PaisEntity> _validator;

    public AdicionarPaisService(IAdicionarPaisRepository adicionarPaisRepository, IValidator<PaisEntity> validator)
    {
        _adicionarPaisRepository = adicionarPaisRepository;
        _validator = validator;
    }

    public async Task<IFinal> AddAsync(PaisDTO dto, CancellationToken cancellationToken)
    {
        PaisEntity entity = new PaisEntity(dto.Nome);

        var validationResult = await _validator.ValidateAsync(entity);

        if (!validationResult.IsValid) return Final.Failure("pais.add.falhaValidacao", "país inválido");

        var result = await _adicionarPaisRepository.AddAsync(entity, cancellationToken);

        if (result > 0) return Final.Success();
        return Final.Failure("pais.add.falha", "não foi possível adicionar o país");
    }
}
