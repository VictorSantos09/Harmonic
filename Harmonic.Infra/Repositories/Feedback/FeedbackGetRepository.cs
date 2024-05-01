using Dapper;
using Harmonic.Domain.Entities.Feedback;
using Harmonic.Infra.Repositories.Feedback.Contracts;
using Harmonic.Shared.Data;
using Harmonic.Shared.Extensions.Collection;
using Microsoft.Extensions.Configuration;
using QuickKit.Builders.ProcedureName.GetAll;
using QuickKit.Builders.ProcedureName.GetById;
using System.Data;


namespace Harmonic.Infra.Repositories.Feedback;

internal class FeedbackGetRepository : Repository, IFeedbackGetRepository
{
    private readonly IProcedureNameBuilderGetAllStrategy _procedureNameBuilderGetAllStrategy;
    private readonly IProcedureNameBuilderGetByIdStrategy _procedureNameBuilderGetByIdStrategy;

    public FeedbackGetRepository(IConfiguration configuration, IProcedureNameBuilderGetByIdStrategy procedureNameBuilderGetByIdStrategy, IProcedureNameBuilderGetAllStrategy procedureNameBuilderGetAllStrategy) : base(configuration)
    {
        _procedureNameBuilderGetByIdStrategy = procedureNameBuilderGetByIdStrategy;
        _procedureNameBuilderGetAllStrategy = procedureNameBuilderGetAllStrategy;
    }

    public async Task<IEnumerable<FeedbackEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        var procedureName = _procedureNameBuilderGetAllStrategy.Build<FeedbackEntity>();

        CommandDefinition command = new(procedureName, commandType: CommandType.StoredProcedure, cancellationToken: cancellationToken);

        IEnumerable<FeedbackSnapshot> snapshots;
        using IDbConnection conn = Connect();
        snapshots = await conn.QueryAsync<FeedbackSnapshot>(command);

        return snapshots.ToEntities<FeedbackEntity, FeedbackSnapshot, int>();
    }

    public async Task<FeedbackEntity?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var procedureName = _procedureNameBuilderGetByIdStrategy.Build<FeedbackEntity>();

        CommandDefinition command = new(
            procedureName, new
            {
                ID_PARAM = id
            }, commandType: CommandType.StoredProcedure, cancellationToken: cancellationToken);

        FeedbackSnapshot? result;
        using IDbConnection conn = Connect();
        result = await conn.QuerySingleOrDefaultAsync<FeedbackSnapshot>(command);

        return FeedbackEntity.FromSnapshot(result);
    }
}
