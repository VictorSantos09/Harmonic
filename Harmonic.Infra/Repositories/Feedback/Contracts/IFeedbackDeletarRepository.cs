using Harmonic.Domain.Entities.Feedback;
using QuickKit.Repositories.Contracts;

namespace Harmonic.Infra.Repositories.Feedback.Contracts;

public interface IFeedbackDeletarRepository : IDeleteRepository<FeedbackEntity, int>
{
}
