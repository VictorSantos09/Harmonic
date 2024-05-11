using Dapper;
using Harmonic.Domain.Entities.Pais;
using Harmonic.Infra.Repositories.Pais.Contracts;
using Harmonic.Shared.Data;
using QuickKit.Builders.ProcedureName.Delete;
using QuickKit.Extensions;
using System.Data;

namespace Harmonic.Infra.Repositories.Pais;



internal class PaisDeletarRepository : Repository, IPaisDeletarRepository
{
    private readonly IProcedureNameBuilderDeleteStrategy _procedureNameBuilderDeleteStrategy;

    public PaisDeletarRepository(IProcedureNameBuilderDeleteStrategy procedureNameBuilderDeleteStrategy,
                                 IDbConnection conn) : base(conn)
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

        return await _connection.ExecuteOnTransactionAsync(command);
    }
}
