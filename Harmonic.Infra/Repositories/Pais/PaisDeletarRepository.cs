using Dapper;
using Harmonic.Domain.Entities.Pais;
using Harmonic.Regras.Contracts.Repositories.Pais;
using Harmonic.Shared.Data;
using QuickKit.Builders.ProcedureName.Delete;
using QuickKit.Extensions;
using System.Data;

namespace Harmonic.Infra.Repositories.Pais;



internal class PaisDeletarRepository : Repository, IPaisDeletarRepository
{
    private readonly IProcedureNameBuilderDeleteStrategy _procedureNameBuilderDeleteStrategy;

    public PaisDeletarRepository(IProcedureNameBuilderDeleteStrategy procedureNameBuilderDeleteStrategy)
    {
        _procedureNameBuilderDeleteStrategy = procedureNameBuilderDeleteStrategy;
    }

    public async Task<int> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var procedureName = _procedureNameBuilderDeleteStrategy.Build<PaisEntity>();

        CommandDefinition command = new(procedureName, new
        {
            IdParam = id
        }, commandType: CommandType.StoredProcedure, cancellationToken: cancellationToken);

        using IDbConnection conn = Connect();
        return await conn.ExecuteOnTransactionAsync(command);
    }
}
