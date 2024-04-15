using Dapper;
using FluentValidation;
using Harmonic.Domain.Entities.Conteudo;
using Harmonic.Regras.Contracts.Repositories.Conteudo;
using Harmonic.Shared.Data;
using QuickKit.Builders.ProcedureName.Add;
using QuickKit.Extensions;
using System.Data;

namespace Harmonic.Infra.Repositories.Conteudo;

internal class AdicionarConteudoRepository : Repository, IAdicionarConteudoRepository
{
    private readonly IProcedureNameBuilderAddStrategy _procedureNameBuilderAddStrategy;
    private readonly IValidator<ConteudoEntity> _validator;

    public AdicionarConteudoRepository(IProcedureNameBuilderAddStrategy procedureNameBuilderAddStrategy, IValidator<ConteudoEntity> validator)
    {
        _procedureNameBuilderAddStrategy = procedureNameBuilderAddStrategy;
        _validator = validator;
    }

    public async Task<int> AddAsync(ConteudoEntity entity, CancellationToken cancellationToken)
    {
        var procedureName = _procedureNameBuilderAddStrategy.Build<ConteudoEntity>();

        object parameters = new
        {
            entity.DataCadastro,
            entity.Descricao,
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
            var validationFailureMessage = "Não foi possível adicionar o conteúdo";
            return await conn.ExecuteValidatingAsync(entity, _validator, validationFailureMessage, command);
        }
    }
}
