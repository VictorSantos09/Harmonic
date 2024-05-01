using Dapper;
using FluentValidation;
using Harmonic.Domain.Entities.Feedback;
using Harmonic.Infra.Repositories.Feedback.Contracts;
using Harmonic.Shared.Data;
using Microsoft.Extensions.Configuration;
using QuickKit.Builders.ProcedureName.Update;
using QuickKit.Extensions;
using System.Data;


namespace Harmonic.Infra.Repositories.Feedback;

internal class FeedbackAtualizarRepository : Repository, IFeedbackAtualizarRepository
{
    private readonly IProcedureNameBuilderUpdateStrategy _procedureNameBuilderUpdateStrategy;
    private readonly IValidator<FeedbackEntity> _validator;

    public FeedbackAtualizarRepository(IProcedureNameBuilderUpdateStrategy procedureNameBuilderUpdateStrategy,
                                       IValidator<FeedbackEntity> validator,
                                       IConfiguration configuration) : base(configuration)
    {
        _procedureNameBuilderUpdateStrategy = procedureNameBuilderUpdateStrategy;
        _validator = validator;
    }

    public async Task<int> UpdateAsync(FeedbackEntity entity, CancellationToken cancellationToken)
    {
        var procedureName = _procedureNameBuilderUpdateStrategy.Build<FeedbackEntity>();

        CommandDefinition command = new(
            procedureName, new
            {
                ID_PARAM = entity.Id,
                TOTAL_CURTIDAS_PARAM = entity.TotalCurtidas,
                TOTAL_GOSTEIS_PARAM = entity.TotalGosteis,
            }, commandType: CommandType.StoredProcedure, cancellationToken: cancellationToken);

        using IDbConnection conn = Connect();
        return await conn.ExecuteValidatingAsync(entity, _validator, "O Feedback é inválido", command);
    }
}
