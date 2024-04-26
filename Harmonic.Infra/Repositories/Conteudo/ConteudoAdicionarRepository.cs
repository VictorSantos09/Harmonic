using Dapper;
using FluentValidation;
using Harmonic.Domain.Entities.Conteudo;
using Harmonic.Infra.Repositories.Contracts.Conteudo;
using Harmonic.Shared.Constants.Base;
using Harmonic.Shared.Data;
using Microsoft.Extensions.Configuration;
using QuickKit.Builders.ProcedureName.Add;
using QuickKit.Extensions;
using System.Data;

namespace Harmonic.Infra.Repositories.Conteudo;

internal class ConteudoAdicionarRepository : Repository, IConteudoAdicionarRepository
{
    private readonly IProcedureNameBuilderAddStrategy _procedureNameBuilderAddStrategy;
    private readonly IValidator<ConteudoEntity> _validator;

    public ConteudoAdicionarRepository(IProcedureNameBuilderAddStrategy procedureNameBuilderAddStrategy, IValidator<ConteudoEntity> validator, IConfiguration configuration) : base(configuration)
    {
        _procedureNameBuilderAddStrategy = procedureNameBuilderAddStrategy;
        _validator = validator;
    }

    public async Task<int> AddAsync(ConteudoEntity entity, CancellationToken cancellationToken)
    {
        var procedureName = _procedureNameBuilderAddStrategy.Build<ConteudoEntity>();

        object parameters = new
        {
            dataCadastroParam = entity.DataCadastro,
            descricaoParam = entity.Descricao,
            idFeedbackParam = entity.Feedback.Id,
            idTipoConteudoParam = entity.TipoConteudo.Id,
            idPaisParam = entity.Pais,
        };

        CommandDefinition command = new(procedureName,
                                        parameters,
                                        commandType: CommandType.StoredProcedure,
                                        cancellationToken: cancellationToken);

        using (IDbConnection conn = Connect())
        {
            return await conn.ExecuteValidatingAsync(entity, _validator, DefaultMessages.INVALID_DATA, command);
        }
    }
}
