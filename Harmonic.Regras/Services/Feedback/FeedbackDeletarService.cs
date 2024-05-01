using Harmonic.Infra.Enums;
using Harmonic.Infra.Repositories.Common;
using Harmonic.Infra.Repositories.Feedback.Contracts;
using Harmonic.Regras.Services.Feedback.Contracts;
using QuickKit.ResultTypes;

namespace Harmonic.Regras.Services.Feedback;

internal class FeedbackDeletarService : IFeedbackDeletarService
{
    private readonly IFeedbackDeletarRepository _feedbackDeletarRepository;
    private readonly IExistsRepository _existsRepository;

    public FeedbackDeletarService(IFeedbackDeletarRepository feedbackDeletarRepository,
                              IExistsRepository existsRepository)
    {
        _feedbackDeletarRepository = feedbackDeletarRepository;
        _existsRepository = existsRepository;
    }

    public async Task<IFinal> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var exists = await _existsRepository.ExistsAsync(TABLES.FEEDBACKS, id, cancellationToken);

        if (!exists) return Final.Failure("feedback.deletar.NaoExiste", "o feedback não foi encontrado");

        var result = await _feedbackDeletarRepository.DeleteAsync(id, cancellationToken);

        if (result > 0) return Final.Success("feedback deletado com sucesso");

        return Final.Failure("feedback.deletar.Falha", "não foi possível deletar o feedback");
    }
}
