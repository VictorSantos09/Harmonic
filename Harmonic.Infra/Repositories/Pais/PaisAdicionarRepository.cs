using Dapper;
using FluentValidation;
using Harmonic.Domain.Entities.Pais;
using Harmonic.Infra.Repositories.Pais.Contracts;
using Harmonic.Shared.Data;
using Microsoft.Extensions.Configuration;
using QuickKit.Builders.ProcedureName.Add;
using QuickKit.Extensions;
using System.Data;

namespace Harmonic.Infra.Repositories.Pais;
internal class PaisAdicionarRepository : Repository, IAdicionarPaisRepository
{
    private readonly IProcedureNameBuilderAddStrategy _procedureNameBuilderAddStrategy;
    private readonly IValidator<PaisEntity> _validator;

    public PaisAdicionarRepository(IProcedureNameBuilderAddStrategy procedureNameBuilderAddStrategy, IValidator<PaisEntity> validator, IConfiguration configuration) : base(configuration)
    {
        _procedureNameBuilderAddStrategy = procedureNameBuilderAddStrategy;
        _validator = validator;
    }

    public async Task<int> AddAsync(PaisEntity entity, CancellationToken cancellationToken)
    {
        var procedureName = _procedureNameBuilderAddStrategy.Build<PaisEntity>();
        object parameters = new
        {
            nomeParam = entity.Nome,
        };

        CommandDefinition command = new(
            procedureName, parameters, commandType: CommandType.StoredProcedure, cancellationToken: cancellationToken);

        using (IDbConnection conn = Connect())
        {
            return await conn.ExecuteValidatingAsync(entity, _validator, "O País informado é inválido", command);
        }
    }
}
