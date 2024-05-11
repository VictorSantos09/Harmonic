using Dapper;
using Harmonic.Domain.Entities.Feedback;
using Harmonic.Infra.Repositories.Feedback.Contracts;
using Harmonic.Shared.Data;
using QuickKit.Builders.ProcedureName.Delete;
using QuickKit.Extensions;
using System.Data;


namespace Harmonic.Infra.Repositories.Feedback;

internal class FeedbackDeletarRepository : Repository, IFeedbackDeletarRepository
{
    private readonly IProcedureNameBuilderDeleteStrategy _procedureNameBuilderDeleteStrategy;

    public FeedbackDeletarRepository(IProcedureNameBuilderDeleteStrategy procedureNameBuilderDeleteStrategy,
                                     IDbConnection connection) : base(connection)
    {
        _procedureNameBuilderDeleteStrategy = procedureNameBuilderDeleteStrategy;
    }

    public async Task<int> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var procedureName = _procedureNameBuilderDeleteStrategy.Build<FeedbackEntity>();

        CommandDefinition command = new(
            procedureName, new
            {
                ID_PARAM = id
            }, commandType: CommandType.StoredProcedure, cancellationToken: cancellationToken);

        return await _connection.ExecuteOnTransactionAsync(command);
    }
}
