using FluentValidation;
using Harmonic.Domain.Entities.ConteudoPlataforma;
using Harmonic.Infra.Repositories.ConteudoPlataforma.Contracts;
using Harmonic.Regras.Services.ConteudoPlataforma.Contracts;
using Harmonic.Regras.Services.ConteudoPlataforma.DTOs;
using QuickKit.ResultTypes;

namespace Harmonic.Regras.Services.ConteudoPlataforma;

internal class ConteudoPlataformaAtualizarService : IConteudoPlataformaAtualizarService
{

    private readonly IConteudoPlataformaAtualizarRepository _atualizarRepository;
    private readonly IValidator<ConteudoPlataformaEntity> _validator;
   
    public ConteudoPlataformaAtualizarService(IConteudoPlataformaAtualizarRepository atualizarRepository,
                                    IValidator<ConteudoPlataformaEntity> validator)
    {
        _atualizarRepository = atualizarRepository;
        _validator = validator;
    }

    public async Task<IFinal> UpdateAsync(ConteudoPlataformaDTO dto, CancellationToken cancellationToken)
    {
        ConteudoPlataformaEntity conteudoPlataforma = new(dto.URL, dto.Conteudo, dto.Plataforma)
        {
            Id = dto.Id
        } ;

        var validationResult = await _validator.ValidateAsync(conteudoPlataforma, cancellationToken);

        if (!validationResult.IsValid) return Final.Failure("conteudo.atualizar.Invalido", "dados do conteúdo são inválidos");

        var result = await _atualizarRepository.UpdateAsync(conteudoPlataforma, cancellationToken);

        if (result > 0) return Final.Success("conteúdo atualizado com sucesso");

        return Final.Failure("ConteudoPlataforma.Update.Falha", "Não foi possível atualizar");
    }

    
}

