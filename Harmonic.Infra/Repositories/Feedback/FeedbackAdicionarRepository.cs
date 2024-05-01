using Dapper;
using FluentValidation;
using Harmonic.Domain.Entities.Feedback;
using Harmonic.Infra.Repositories.Feedback.Contracts;
using Harmonic.Shared.Constants.Base;
using Harmonic.Shared.Data;
using Microsoft.Extensions.Configuration;
using QuickKit.Builders.ProcedureName.Add;
using QuickKit.Extensions;
using System.Data;

namespace Harmonic.Infra.Repositories.Feedback;

internal class FeedbackAdicionarRepository : Repository, IFeedbackAdicionarRepository
{
    private readonly IProcedureNameBuilderAddStrategy _procedureNameBuilderAddStrategy;
    private readonly IValidator<FeedbackEntity> _validator;

    public FeedbackAdicionarRepository(IProcedureNameBuilderAddStrategy procedureNameBuilderAddStrategy,
                                       IValidator<FeedbackEntity> validator,
                                       IConfiguration configuration) : base(configuration)
    {
        _procedureNameBuilderAddStrategy = procedureNameBuilderAddStrategy;
        _validator = validator;
    }

    public async Task<int> AddAsync(FeedbackEntity entity, CancellationToken cancellationToken)
    {
        var procedureName = _procedureNameBuilderAddStrategy.Build<FeedbackEntity>();

        object parameters = new
        {
            TOTAL_CURTIDAS_PARAM = entity.TotalCurtidas,
            TOTAL_GOSTEIS_PARAM = entity.TotalGosteis,
            
        };

        CommandDefinition command = new(procedureName,
                                        parameters,
                                        commandType: CommandType.StoredProcedure,
                                        cancellationToken: cancellationToken);

        using (IDbConnection conn = Connect())
        {
            return await conn.ExecuteValidatingAsync(entity, _validator, "O feedback é inválido", command);
        }
    }
}

