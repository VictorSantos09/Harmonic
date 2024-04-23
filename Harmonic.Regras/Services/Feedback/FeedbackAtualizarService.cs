using FluentValidation;
using Harmonic.Domain.Entities.Feedback;
using Harmonic.Infra.Enums;
using Harmonic.Infra.Repositories.Contracts.Common;
using Harmonic.Infra.Repositories.Contracts.Feedback;
using Harmonic.Regras.Services.Conteudo.DTOs;
using Harmonic.Regras.Services.Feedback.Contracts;
using QuickKit.ResultTypes;

namespace Harmonic.Regras.Services.Feedback;

internal class FeedbackAtualizarService : IFeedbackAtualizarService
{
    private readonly IFeedbackAtualizarRepository _feedbackAtualizarRepository;
    private readonly IExistsRepository _existsRepository;
    private readonly IValidator<FeedbackEntity> _validator;

    public FeedbackAtualizarService(IFeedbackAtualizarRepository feedbackAtualizarRepository,
                                IExistsRepository existsRepository,
                                IValidator<FeedbackEntity> validator)
    {
        _feedbackAtualizarRepository = feedbackAtualizarRepository;
        _existsRepository = existsRepository;
        _validator = validator;
    }

    public async Task<IFinal> UpdateAsync(FeedbackDTO dto, CancellationToken cancellationToken)
    {
        var exists = await _existsRepository.ExistsAsync(TABLES.FEEDBACKS, dto.Id, cancellationToken);

        if (!exists) return Final.Failure("feedback.atualizar.NaoExiste", "o feedback não foi encontrado");

        FeedbackEntity feedback = new(dto.TotalCurtidas, dto.TotalGosteis);

        var validationResult = await _validator.ValidateAsync(feedback, cancellationToken);

        if (!validationResult.IsValid) return Final.Failure("feedback.atualizar.Invalido", "dados inválidos foram informados");

        var result = await _feedbackAtualizarRepository.UpdateAsync(feedback, cancellationToken);

        if (result > 0) return Final.Success("feedback atualizado com sucesso");

        return Final.Failure("feedback.atualizar.Falha", "não foi possível atualizar o feedback");
    }
}
