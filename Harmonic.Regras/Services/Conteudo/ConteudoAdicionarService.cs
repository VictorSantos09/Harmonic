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

internal class ConteudoAdicionarService : IConteudoAdicionarService
{
    private readonly IConteudoAdicionarRepository _adicionarConteudoRepository;
    private readonly IPaisGetService _paisGetService;
    private readonly IFeedbackGetService _feedbackGetService;
    private readonly IValidator<ConteudoEntity> _validator;

    public ConteudoAdicionarService(IConteudoAdicionarRepository adicionarConteudoRepository,
                                    IPaisGetService paisGetService,
                                    IFeedbackGetService feedbackGetService,
                                    IValidator<ConteudoEntity> validator)
    {
        _adicionarConteudoRepository = adicionarConteudoRepository;
        _paisGetService = paisGetService;
        _feedbackGetService = feedbackGetService;
        _validator = validator;
    }

    public async Task<IFinal> AddAsync(ConteudoDTO dto, CancellationToken cancellationToken)
    {
        TipoConteudoEntity tipoConteudo = new(dto.TipoConteudo.Nome) { Id = dto.TipoConteudo.Id };
        
        var pais = await _paisGetService.GetByIdAsync(dto.IdPais, cancellationToken);

        if (pais is null) return Final.Failure("Conteudo.Add.Falha", "Não foi possível adicionar o conteúdo, pais não encontrado");

        FeedbackEntity feedback = new(0, 0);

        ConteudoEntity entity = new(dto.Titulo, dto.DataCadastro, dto.Descricao, tipoConteudo, pais.Data!, feedback);

        var validationResult = await _validator.ValidateAsync(entity, cancellationToken);

        if (!validationResult.IsValid) return Final.Failure("Conteudo.Add.Invalido", "Dados do conteúdo são inválidos");

        int result = await _adicionarConteudoRepository.AddAsync(entity, cancellationToken);

        if (result > 0) return Final.Success();

        return Final.Failure("Conteudo.Add.Falha", "Não foi possível adicionar o conteúdo");
    }
}
