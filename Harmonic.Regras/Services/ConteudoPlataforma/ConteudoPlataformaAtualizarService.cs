using FluentValidation;
using Harmonic.Domain.Entities.ConteudoPlataforma;
using Harmonic.Infra.Repositories.ConteudoPlataforma.Contracts;
using Harmonic.Regras.Services.Conteudo.Contracts;
using Harmonic.Regras.Services.ConteudoPlataforma.Contracts;
using Harmonic.Regras.Services.ConteudoPlataforma.DTOs;
using Harmonic.Regras.Services.Plataforma.Contracts;
using QuickKit.ResultTypes;

namespace Harmonic.Regras.Services.ConteudoPlataforma;

internal class ConteudoPlataformaAtualizarService : IConteudoPlataformaAtualizarService
{
    private readonly IConteudoPlataformaAtualizarRepository _atualizarRepository;
    private readonly IValidator<ConteudoPlataformaEntity> _validator;
    private readonly IConteudoGetService _conteudoGetService;
    private readonly IPlataformaGetService _plataformaGetService;

    public ConteudoPlataformaAtualizarService(IConteudoPlataformaAtualizarRepository atualizarRepository,
                                              IValidator<ConteudoPlataformaEntity> validator,
                                              IConteudoGetService conteudoGetService,
                                              IPlataformaGetService plataformaGetService)
    {
        _atualizarRepository = atualizarRepository;
        _validator = validator;
        _conteudoGetService = conteudoGetService;
        _plataformaGetService = plataformaGetService;
    }

    public async Task<IFinal> UpdateAsync(ConteudoPlataformaDTO dto, CancellationToken cancellationToken)
    {
        var conteudoResult = await _conteudoGetService.GetByIdAsync(dto.IdConteudo, cancellationToken);
        var plataformaResult = await _plataformaGetService.GetByIdAsync(dto.IdPlataforma, cancellationToken);

        if (conteudoResult.IsFailure) return conteudoResult;
        if (plataformaResult.IsFailure) return plataformaResult;

        ConteudoPlataformaEntity conteudoPlataforma = new(dto.URL, conteudoResult.Data!, plataformaResult.Data!)
        {
            Id = dto.Id
        };

        var validationResult = await _validator.ValidateAsync(conteudoPlataforma, cancellationToken);

        if (!validationResult.IsValid) return Final.Failure("conteudo.atualizar.Invalido", "dados do conteúdo são inválidos");

        var result = await _atualizarRepository.UpdateAsync(conteudoPlataforma, cancellationToken);

        if (result > 0) return Final.Success("conteúdo atualizado com sucesso");

        return Final.Failure("ConteudoPlataforma.Update.Falha", "Não foi possível atualizar");
    }
}

