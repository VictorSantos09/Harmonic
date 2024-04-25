using FluentValidation;
using Harmonic.Domain.Entities.Conteudo;
using Harmonic.Domain.Entities.Feedback;
using Harmonic.Domain.Entities.Pais;
using Harmonic.Domain.Entities.TipoConteudo;
using Harmonic.Infra.Repositories.Contracts.Conteudo;
using Harmonic.Regras.Services.Conteudo.Contracts;
using Harmonic.Regras.Services.Conteudo.DTOs;
using QuickKit.ResultTypes;

namespace Harmonic.Regras.Services.Conteudo;

internal class ConteudoAtualizarService : IConteudoAtualizarService
{
    private readonly IConteudoAtualizarRepository _conteudoAtualizarRepository;
    private readonly IValidator<ConteudoEntity> _validator;

    public ConteudoAtualizarService(IConteudoAtualizarRepository conteudoAtualizarRepository,
                                    IValidator<ConteudoEntity> validator)
    {
        _conteudoAtualizarRepository = conteudoAtualizarRepository;
        _validator = validator;
    }

    public async Task<IFinal> UpdateAsync(ConteudoDTO dto, CancellationToken cancellationToken)
    {
        TipoConteudoEntity tipoConteudo = new(dto.TipoConteudo.Nome);
        PaisEntity pais = new(dto.Pais.Nome);
        FeedbackEntity feedback = new(dto.Feedback.TotalCurtidas, dto.Feedback.TotalGosteis);

        ConteudoEntity conteudo = new (dto.Titulo, dto.DataCadastro, dto.Descricao, tipoConteudo, pais, feedback);

        var validationResult = await _validator.ValidateAsync(conteudo, cancellationToken);

        if (!validationResult.IsValid) return Final.Failure("conteudo.atualizar.Invalido", "dados do conteúdo são inválidos");

        var result = await _conteudoAtualizarRepository.UpdateAsync(conteudo, cancellationToken);

        if (result > 0) return Final.Success("conteúdo atualizado com sucesso");

        return Final.Failure("conteudo.atualizar.Falha", "não foi possível atualizar o conteúdo");
    }
}
