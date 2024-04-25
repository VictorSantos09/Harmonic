using Harmonic.Domain.Entities.Feedback;
using QuickKit.Repositories.Contracts;

namespace Harmonic.Infra.Repositories.Contracts.Feedback;

public interface IFeedbackGetRepository : IGetAllRepository<FeedbackEntity>, IGetByIdRepository<FeedbackEntity, int>
{
}
