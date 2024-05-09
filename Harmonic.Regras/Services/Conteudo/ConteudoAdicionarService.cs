using FluentValidation;
using Harmonic.Domain.Entities.Conteudo;
using Harmonic.Domain.Entities.Feedback;
using Harmonic.Domain.Entities.Pais;
using Harmonic.Domain.Entities.TipoConteudo;
using Harmonic.Infra.Repositories.Conteudo.Contracts;
using Harmonic.Infra.Repositories.Feedback.Contracts;
using Harmonic.Regras.Services.Conteudo.Contracts;
using Harmonic.Regras.Services.Conteudo.DTOs;
using Harmonic.Regras.Services.Feedback.Contracts;
using Harmonic.Regras.Services.Pais.Contracts;
using Harmonic.Regras.Services.TipoConteudo.Contracts;
using QuickKit.ResultTypes;

namespace Harmonic.Regras.Services.Conteudo;

internal class ConteudoAdicionarService : IConteudoAdicionarService
{
    private readonly IConteudoAdicionarRepository _adicionarConteudoRepository;
    private readonly IPaisGetService _paisGetService;
    private readonly IFeedbackGetService _feedbackGetService;
    private readonly IFeedbackAdicionarRepository _adicionarFeedbackRepository;
    private readonly IValidator<ConteudoEntity> _validator;
    private readonly ITipoConteudoGetServices _tipoConteudoService;

    public ConteudoAdicionarService(IConteudoAdicionarRepository adicionarConteudoRepository,
                                    IPaisGetService paisGetService,
                                    IFeedbackGetService feedbackGetService,
                                    IFeedbackAdicionarRepository adicionarFeedbackRepository,
                                    ITipoConteudoGetServices tipoConteudoService,
                                    IValidator<ConteudoEntity> validator)
    {
        _adicionarConteudoRepository = adicionarConteudoRepository;
        _paisGetService = paisGetService;
        _feedbackGetService = feedbackGetService;
        _adicionarFeedbackRepository = adicionarFeedbackRepository;
        _tipoConteudoService = tipoConteudoService;
        _validator = validator;
    }

    public async Task<IFinal> AddAsync(ConteudoDTO dto, CancellationToken cancellationToken)
    {
        
        var tipoConteudo = await _tipoConteudoService.GetByIdAsync(dto.ID_TIPO_CONTEUDO, cancellationToken);

        if (tipoConteudo is null) return Final.Failure("Conteudo.Add.Falha", "Não foi possível adicionar o conteúdo, pais não encontrado");

        var pais = await _paisGetService.GetByIdAsync(dto.ID_PAIS, cancellationToken);

        if (pais is null) return Final.Failure("Conteudo.Add.Falha", "Não foi possível adicionar o conteúdo, pais não encontrado");

        FeedbackEntity feedback = new(0, 0);

        await _adicionarFeedbackRepository.AddAsync(feedback, cancellationToken);

        ConteudoEntity entity = new( dto.Id, dto.Titulo, dto.DataCadastro, dto.Descricao, dto.ID_FEEDBACK, dto.ID_PAIS, dto.ID_TIPO_CONTEUDO, tipoConteudo.Data!, pais.Data!, feedback);

        var validationResult = await _validator.ValidateAsync(entity, cancellationToken);

        if (!validationResult.IsValid) return Final.Failure("Conteudo.Add.Invalido", "Dados do conteúdo são inválidos");

        int result = await _adicionarConteudoRepository.AddAsync(entity, cancellationToken);

        if (result > 0) return Final.Success();

        return Final.Failure("Conteudo.Add.Falha", "Não foi possível adicionar o conteúdo");
    }
}
