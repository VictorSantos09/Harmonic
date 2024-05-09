using FluentValidation;
using Harmonic.Domain.Entities.Conteudo;
using Harmonic.Domain.Entities.Feedback;
using Harmonic.Domain.Entities.Pais;
using Harmonic.Domain.Entities.TipoConteudo;
using Harmonic.Infra.Repositories.Conteudo.Contracts;
using Harmonic.Regras.Services.Conteudo.Contracts;
using Harmonic.Regras.Services.Conteudo.DTOs;
using Harmonic.Regras.Services.Feedback.Contracts;
using Harmonic.Regras.Services.Pais.Contracts;
using QuickKit.ResultTypes;

namespace Harmonic.Regras.Services.Conteudo;

internal class ConteudoAtualizarService : IConteudoAtualizarService
{
    private readonly IConteudoAtualizarRepository _conteudoAtualizarRepository;
    private readonly IValidator<ConteudoEntity> _validator;
    private readonly IPaisGetService _paisGetService;
    private readonly IFeedbackGetService _feedbackGetService;

    public ConteudoAtualizarService(IConteudoAtualizarRepository conteudoAtualizarRepository,
                                    IValidator<ConteudoEntity> validator,
                                    IPaisGetService paisGetService,
                                    IFeedbackGetService feedbackGetService)
    {
        _conteudoAtualizarRepository = conteudoAtualizarRepository;
        _validator = validator;
        _paisGetService = paisGetService;
        _feedbackGetService = feedbackGetService;
    }

    public async Task<IFinal> UpdateAsync(ConteudoDTO dto, CancellationToken cancellationToken)
    {
        var pais = await _paisGetService.GetByIdAsync(dto.ID_PAIS, cancellationToken);
        var feedback = await _feedbackGetService.GetByIdAsync(dto.Feedback.Id, cancellationToken);

        if (pais is null) return Final.Failure("conteudo.atualizar.PaisNaoEncontrado", "país não encontrado");
        if (feedback is null) return Final.Failure("conteudo.atualizar.FeedbackNaoEncontrado", "feedback não encontrado");

        feedback.Data.TotalGosteis = dto.Feedback.TotalGosteis;
        feedback.Data.TotalCurtidas = dto.Feedback.TotalCurtidas;

        TipoConteudoEntity tipoConteudo = new(dto.TipoConteudo.Nome)
        {
            Id = dto.TipoConteudo.Id
        };

        ConteudoEntity conteudo = new(dto.Id,
            dto.Titulo,
                                      dto.DataCadastro,
                                      dto.Descricao,
                                      dto.ID_FEEDBACK,
                                      dto.ID_PAIS,
                                      dto.ID_TIPO_CONTEUDO,
                                      tipoConteudo,
                                      pais.Data,
                                      feedback.Data) { Id = dto.Id };

        var validationResult = await _validator.ValidateAsync(conteudo, cancellationToken);

        if (!validationResult.IsValid) return Final.Failure("conteudo.atualizar.Invalido", "dados do conteúdo são inválidos");

        var result = await _conteudoAtualizarRepository.UpdateAsync(conteudo, cancellationToken);

        if (result > 0) return Final.Success("conteúdo atualizado com sucesso");

        return Final.Failure("conteudo.atualizar.Falha", "não foi possível atualizar o conteúdo");
    }
}
