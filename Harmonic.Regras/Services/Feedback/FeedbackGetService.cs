using Harmonic.Domain.Entities.Feedback;
using Harmonic.Infra.Repositories.Contracts.Feedback;
using Harmonic.Regras.Services.Feedback.Contracts;
using QuickKit.ResultTypes;
using QuickKit.Shared.Extensions;

namespace Harmonic.Regras.Services.Feedback;

internal class FeedbackGetService : IFeedbackGetService
{
    private readonly IFeedbackGetRepository _feedbackGetRepository;

    public FeedbackGetService(IFeedbackGetRepository feedbackGetRepository)
    {
        _feedbackGetRepository = feedbackGetRepository;
    }

    public async Task<IFinal<IEnumerable<FeedbackEntity>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var result = await _feedbackGetRepository.GetAllAsync(cancellationToken);
        return Final.Success(result);
    }

    public async Task<IFinal<FeedbackEntity?>> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var result = await _feedbackGetRepository.GetByIdAsync(id, cancellationToken);
        if (result.IsNull()) return Final.Failure(result, "feedback.get.NaoEncontrado", "O feedback não foi encontrado");

        return Final.Success(result);
    }
}
