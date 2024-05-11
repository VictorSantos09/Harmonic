using Dapper;
using FluentValidation;
using Harmonic.Domain.Entities.Pais;
using Harmonic.Infra.Repositories.Pais.Contracts;
using Harmonic.Shared.Data;
using MySqlX.XDevAPI.Relational;
using QuickKit.Builders.ProcedureName.Add;
using QuickKit.Extensions;
using System.Data;
using System.Threading;

namespace Harmonic.Infra.Repositories.Pais;
internal class PaisAdicionarRepository : Repository, IAdicionarPaisRepository
{
    private readonly IProcedureNameBuilderAddStrategy _procedureNameBuilderAddStrategy;
    private readonly IValidator<PaisEntity> _validator;

    public PaisAdicionarRepository(IProcedureNameBuilderAddStrategy procedureNameBuilderAddStrategy,
                                   IValidator<PaisEntity> validator,
                                   IDbConnection conn) : base(conn)
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

            return await _connection.ExecuteValidatingAsync(entity, _validator, "O País informado é inválido", command);
    }

    public async Task<bool> ExistsByName(string name, CancellationToken cancellationToken)
    {
        string sql = $"SELECT IF((SELECT COUNT(1) FROM PAISES WHERE NOME = @name), true, false) AS RESULT;";

        CommandDefinition command = new(sql, new
        {
            name
        }, cancellationToken: cancellationToken);


        return await _connection.ExecuteScalarAsync<bool>(command);
    }
}
