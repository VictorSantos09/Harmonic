using Harmonic.Domain.Entities.Feedback;
using Harmonic.Regras.Services.Conteudo.DTOs;
using QuickKit.ResultTypes.Services.Contracts;

namespace Harmonic.Regras.Services.Feedback.Contracts;

public interface IFeedbackAdicionarService : IAddService<FeedbackDTO>
{

}

public interface IFeedbackDeletarService : IDeleteService<int>
{

}

public interface IFeedbackAtualizarService : IUpdateService<FeedbackDTO>
{

}

public interface IFeedbackGetService : IGetAllService<FeedbackEntity>, IGetByIdService<FeedbackEntity, int>
{

}