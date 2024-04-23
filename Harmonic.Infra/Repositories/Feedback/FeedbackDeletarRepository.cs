using Dapper;
using Harmonic.Domain.Entities.Feedback;
using Harmonic.Infra.Repositories.Contracts.Feedback;
using Harmonic.Shared.Data;
using Microsoft.Extensions.Configuration;
using QuickKit.Builders.ProcedureName.Delete;
using QuickKit.Extensions;
using System.Data;


namespace Harmonic.Infra.Repositories.Feedback;

internal class FeedbackDeletarRepository : Repository, IFeedbackDeletarRepository
{
    private readonly IProcedureNameBuilderDeleteStrategy _procedureNameBuilderDeleteStrategy;

    public FeedbackDeletarRepository(IProcedureNameBuilderDeleteStrategy procedureNameBuilderDeleteStrategy,
                                     IConfiguration configuration) : base(configuration)
    {
        _procedureNameBuilderDeleteStrategy = procedureNameBuilderDeleteStrategy;
    }

    public Task<int> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var procedureName = _procedureNameBuilderDeleteStrategy.Build<FeedbackEntity>();

        CommandDefinition command = new(
            procedureName, new
            {
                idParam = id
            }, commandType: CommandType.StoredProcedure, cancellationToken: cancellationToken);

        using IDbConnection conn = Connect();
        return conn.ExecuteOnTransactionAsync(command);
    }
}
