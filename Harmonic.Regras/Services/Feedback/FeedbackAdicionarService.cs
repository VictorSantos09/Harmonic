using FluentValidation;
using Harmonic.Domain.Entities.Feedback;
using Harmonic.Infra.Repositories.Feedback.Contracts;
using Harmonic.Regras.Services.Conteudo.DTOs;
using Harmonic.Regras.Services.Feedback.Contracts;
using QuickKit.ResultTypes;


namespace Harmonic.Regras.Services.Feedback;

internal class FeedbackAdicionarService : IFeedbackAdicionarService
{
    private readonly IFeedbackAdicionarRepository _adicionarFeedbackRepository;
    private readonly IValidator<FeedbackEntity> _validator;

    public FeedbackAdicionarService(IFeedbackAdicionarRepository adicionarFeedbackRepository, IValidator<FeedbackEntity> validator)
    {
        _adicionarFeedbackRepository = adicionarFeedbackRepository;
        _validator = validator;
    }

    public async Task<IFinal> AddAsync(FeedbackDTO dto, CancellationToken cancellationToken)
    {
        FeedbackEntity entity = new(dto.TotalCurtidas, dto.TotalGosteis) { Id = dto.Id };


        var validationResult = await _validator.ValidateAsync(entity, cancellationToken);


        if (!validationResult.IsValid) return Final.Failure("feedback.add.falhaValidacao", "feedback inválido");


        var result = await _adicionarFeedbackRepository.AddAsync(entity, cancellationToken);


        if (result > 0) return Final.Success();
        return Final.Failure("feedback.add.falha", "não foi possível adicionar o feedback");
    }
}